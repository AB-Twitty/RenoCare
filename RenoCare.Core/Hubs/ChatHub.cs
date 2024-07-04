using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Core.Features.Authentication.Contracts;
using RenoCare.Core.Hubs.Models;
using RenoCare.Domain;
using RenoCare.Domain.Chat;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RenoCare.Core.Hubs
{
    [Authorize(Roles = "HealthCare, Patient")]
    public class ChatHub : Hub
    {
        private readonly IRepository<ChatMessage> _msgRepo;
        private readonly IAuthService _authService;
        private readonly IRepository<DialysisUnit> _unitRepo;

        public ChatHub(IRepository<ChatMessage> msgRepo, IAuthService authService, IRepository<DialysisUnit> unitRepo)
        {
            _msgRepo = msgRepo;
            _authService = authService;
            _unitRepo = unitRepo;
        }


        public async Task SendMessage(string receiverId, string message)
        {
            try
            {
                var senderId = Context.UserIdentifier;
                var sender = await _authService.GetUserByIdAsync(senderId);

                var receiver = await _authService.GetUserByIdAsync(receiverId);

                bool is_allowed = (await _authService.IsUserInRole(sender, "Patient") && await _authService.IsUserInRole(receiver, "HealthCare"))
                        || (await _authService.IsUserInRole(receiver, "Patient") && await _authService.IsUserInRole(sender, "HealthCare"));

                if (!is_allowed)
                    return;

                var msg = new ChatMessage
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Message = message,
                    SendingTime = DateTime.Now,
                    Status = 1
                };

                await _msgRepo.InsertAsync(msg);
                await _msgRepo.SaveAsync();

                string sender_name = sender.FirstName + " " + sender.LastName;

                if (await _authService.IsUserInRole(sender, "HealthCare"))
                {
                    sender_name = await _unitRepo.Table.Where(x => x.UserId == senderId)
                        .Select(x => x.Name).FirstOrDefaultAsync() ?? sender_name;
                }

                await Clients.Users(receiverId, senderId)
                    .SendAsync("ReceiveMessage", msg, sender_name);
            }
            catch
            {

            }
        }


        public async Task MarkRead(int msgId)
        {
            try
            {
                var receiverId = Context.UserIdentifier;
                var msg = await _msgRepo.ApplyQueryAsync(async qry =>
                    await qry.Where(m => m.Id == msgId && m.ReceiverId == receiverId).FirstOrDefaultAsync());

                if (msg == null)
                    return;

                msg.Status = 3;
                await _msgRepo.UpdateAsync(msg);
                await _msgRepo.SaveAsync();

                await Clients.User(msg.SenderId)
                    .SendAsync("MarkedAsRead", msg.Id);
            }
            catch
            {

            }
        }

        public async Task MarkReceived(int msgId)
        {
            try
            {
                var receiverId = Context.UserIdentifier;
                var msg = await _msgRepo.ApplyQueryAsync(async qry =>
                    await qry.Where(m => m.Id == msgId && m.ReceiverId == receiverId).FirstOrDefaultAsync());

                if (msg == null)
                    return;

                msg.Status = 2;
                await _msgRepo.UpdateAsync(msg);
                await _msgRepo.SaveAsync();

                await Clients.User(msg.SenderId)
                    .SendAsync("MarkedAsReceived", msg);
            }
            catch
            {

            }
        }

        public async Task NotifyPatient(Notification notification)
        {
            await Clients.User(notification.UserId).SendAsync("OnNotified", notification);
        }


        public override async Task OnConnectedAsync()
        {
            try
            {
                var curr_user = Context.UserIdentifier;

                var msgs = await _msgRepo.GetAllAsync(qry =>
                    qry.Where(x => x.ReceiverId == curr_user && x.Status == 1));

                foreach (var msg in msgs)
                {
                    msg.Status = 2;
                    await _msgRepo.UpdateAsync(msg);
                    await _msgRepo.SaveAsync();

                    await Clients.User(msg.SenderId)
                            .SendAsync("MarkedAsReceived", msg);
                }
            }
            catch
            {

            }

            await base.OnConnectedAsync();
        }
    }
}

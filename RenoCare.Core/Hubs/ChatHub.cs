using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain.Chat;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RenoCare.Core.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IRepository<ChatMessage> _msgRepo;

        public ChatHub(IRepository<ChatMessage> msgRepo)
        {
            _msgRepo = msgRepo;
        }


        public async Task SendMessage(string receiverId, string message)
        {
            try
            {
                var senderId = Context.UserIdentifier;

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

                await Clients.Users(receiverId, senderId)
                    .SendAsync("ReceiveMessage", msg);
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

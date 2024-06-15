using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RenoCare.Core.Conatracts.Persistence;
using RenoCare.Domain.Chat;
using System;
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
                    SendingTime = DateTime.Now
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

        public async Task UploadFile(string receiverId, string fileName, byte[] stream)
        {
            try
            {
                var senderId = Context.UserIdentifier;
                if (string.IsNullOrEmpty(senderId))
                {
                    throw new InvalidOperationException("Sender ID cannot be null or empty.");
                }

                var msg = new ChatMessage
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Message = "tis is the file name",
                    SendingTime = DateTime.Now
                };

                // Notify clients about the new file
                await Clients.Users(receiverId, senderId).SendAsync("ReceiveFile", msg, fileName, $"/uploads/{fileName}");
            }
            catch (Exception ex)
            {

                throw; // Re-throw the exception to be handled by the SignalR middleware
            }
        }
    }
}

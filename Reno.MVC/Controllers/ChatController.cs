using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reno.MVC.Models.Chat;
using Reno.MVC.Services.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Reno.MVC.Controllers
{
    public class ChatController : Controller
    {
        private readonly IClient _client;

        public ChatController(IClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = (await _client.GetUserContactsAsync()).Data;

            var active_contact = contacts.FirstOrDefault();

            var messages = new List<ChatMessage>();

            var model = new ChatModel
            {
                Contacts = contacts,
                Messages = messages
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return Ok(new
                {
                    fileName = file.FileName,
                    fileData = memoryStream.ToArray()
                });
            }
        }
    }
}

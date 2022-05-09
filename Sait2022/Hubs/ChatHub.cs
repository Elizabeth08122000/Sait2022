using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Sait2022.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Sait2022.Hubs
{
        [Authorize]
        public class ChatH : Hub
        {
        string groupname = "all";
        //public async Task Enter(string username)
        //{
        //    if (String.IsNullOrEmpty(username))
        //    {
        //        await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
        //    }
        //    else
        //    {
        //        await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        //        await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
        //    }
        //}
        public async Task Send(string message, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
          //  await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
            await Clients.Group(groupname).SendAsync("Receive", message, username);
        }
    }
    }

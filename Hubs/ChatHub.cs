﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace Ciurca_Radu_Lab2.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }
    }
}
using ChatApplication.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendPrivateMessage(messege Message) =>
            await Clients.User(Message.receiverId).SendAsync("receiveMessage", Message);
    }
}

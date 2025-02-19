using Microsoft.AspNetCore.SignalR;

namespace Register.Models.Entities
{
    public sealed class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }
    }
}

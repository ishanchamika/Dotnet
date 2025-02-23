using Microsoft.AspNetCore.SignalR;

namespace Register.Models.Entities
{
    public sealed class ChatHub : Hub
    {
        public async Task JoinChat(UserConnections userConnections)
        {
            await Clients.All.SendAsync("ReceivedMessage", "admin", $"{userConnections.Username} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnections userConnections)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnections.ChatRoom);
            await Clients.Group(userConnections.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{userConnections.Username} has joined {userConnections.ChatRoom}");
        }
    }
}

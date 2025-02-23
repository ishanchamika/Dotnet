using Microsoft.AspNetCore.SignalR;
using Register.Data;

namespace Register.Models.Entities
{
    public sealed class ChatHub : Hub
    {
        private readonly SharedDb _shared;
        public ChatHub(SharedDb shared) => _shared = shared;
        public async Task JoinChat(UserConnections userConnections)
        {
            await Clients.All.SendAsync("ReceivedMessage", "admin", $"{userConnections.Username} has joined");
        }

        public async Task JoinSpecificChatRoom(UserConnections userConnections)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnections.ChatRoom);
            _shared.connections[Context.ConnectionId] = userConnections;
            await Clients.Group(userConnections.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{userConnections.Username} has joined {userConnections.ChatRoom}");
        }

        public async Task SendMessage(string msg)
        {
            if(_shared.connections.TryGetValue(Context.ConnectionId, out UserConnections userConnections))
            {
                await Clients.Group(userConnections.ChatRoom).SendAsync("ReceiveSpecificMessage", userConnections.Username, msg);
            }
        }
    }
}

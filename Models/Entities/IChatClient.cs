namespace Register.Models.Entities
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}

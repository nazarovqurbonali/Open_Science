namespace Domain.Entities;
public class Chat
{
    public int ChatId { get; set; }
    public string SendUserId { get; set; } = null!;
    public User SendUser { get; set; }= null!;
    public string ReceiveUserId { get; set; }= null!;
    public User ReceiveUser { get; set; }= null!;
    public List<Message>? Messages { get; set; }
}
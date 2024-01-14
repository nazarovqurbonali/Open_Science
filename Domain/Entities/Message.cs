
namespace Domain.Entities;

public class Message
{
    public int MessageId { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; }= null!;
    public string UserId { get; set; }= null!;
    public User? User { get; set; }
    public string MessageText { get; set; }= null!;
    public DateTime SendMassageDate { get; set; }
}
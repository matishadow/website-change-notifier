namespace WebsiteChangeNotifier.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string subject, string content);
    }
}
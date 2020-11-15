using System;

namespace WebsiteChangeNotifier.Data
{
    public class EmailConfig
    {
        public string Login { get; set; }
        public string Password { get; set; }
        
        public string Receiver { get; set; }
        
        public string SmtpHost { get; set; }
    }
}
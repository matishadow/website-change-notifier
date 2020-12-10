using System.Collections.Generic;

namespace WebsiteChangeNotifier.Data
{
    public class NotificationConfig
    {
        public string WebsiteUri { get; set; }
        public List<string> Keyword { get; set; }
    }
}
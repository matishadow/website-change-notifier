using System;
using System.Threading.Tasks;

namespace WebsiteChangeNotifier.Interfaces
{
    public interface IWebsiteContentChecker
    {
        Task<bool> IsStringInWebsiteContent(Uri websiteUri, string stringToCheck);
    }
}
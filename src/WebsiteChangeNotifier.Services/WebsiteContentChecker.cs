using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebsiteChangeNotifier.Interfaces;

namespace WebsiteChangeNotifier.Services
{
    public class WebsiteContentChecker : IWebsiteContentChecker
    {
        private readonly HttpClient _httpClient;

        public WebsiteContentChecker(HttpMessageHandler? httpMessageHandler = null)
        {
            httpMessageHandler ??= new HttpClientHandler();
            _httpClient = new HttpClient(httpMessageHandler);
        }

        public async Task<bool> IsStringInWebsiteContent(Uri websiteUri, string stringToCheck)
        {
            var response = await _httpClient.GetAsync(websiteUri);
            var content = await response.Content.ReadAsStringAsync();

            return content.Contains(stringToCheck, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
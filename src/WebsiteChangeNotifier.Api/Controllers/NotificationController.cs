using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebsiteChangeNotifier.Data;
using WebsiteChangeNotifier.Interfaces;

namespace WebsiteChangeNotifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IEmailSender _emailSender;
        private IWebsiteContentChecker _websiteContentChecker;

        private List<NotificationConfig> _notificationConfigList;

        public NotificationController(
            IWebsiteContentChecker websiteContentChecker,
            IEmailSender emailSender,
            List<NotificationConfig> notificationConfigList)
        {
            _websiteContentChecker = websiteContentChecker;
            _emailSender = emailSender;
            _notificationConfigList = notificationConfigList;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            foreach (var notificationConfig in _notificationConfigList)
            {
                foreach (var stringToCheck in notificationConfig.Keyword)
                {
                    var isKeywordInWebsiteContent =
                        await _websiteContentChecker.IsStringInWebsiteContent(new Uri(notificationConfig.WebsiteUri),
                            stringToCheck);

                    if (isKeywordInWebsiteContent)
                        _emailSender.SendEmail("Website Change Notification", $"{stringToCheck} is on {notificationConfig.WebsiteUri}!");

                }
            }

            return Ok();
        }
    }
}
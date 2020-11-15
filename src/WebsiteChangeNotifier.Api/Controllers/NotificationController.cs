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

        private NotificationConfig _notificationConfig;

        public NotificationController(
            IWebsiteContentChecker websiteContentChecker, 
            IEmailSender emailSender, 
            NotificationConfig notificationConfig)
        {
            _websiteContentChecker = websiteContentChecker;
            _emailSender = emailSender;
            _notificationConfig = notificationConfig;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isKeywordInWebsiteContent =
                await _websiteContentChecker.IsStringInWebsiteContent(new Uri(_notificationConfig.WebsiteUri),
                    _notificationConfig.Keyword);

            if (isKeywordInWebsiteContent)
                _emailSender.SendEmail("Website Change Notification", $"{_notificationConfig.Keyword} is on the menu!");

            return Ok();
        }
    }
}
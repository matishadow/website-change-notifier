using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using WebsiteChangeNotifier.Interfaces;
using WebsiteChangeNotifier.Services;

namespace WebsiteChangeNotifier.Tests
{
    public class WebsiteContentCheckerTests
    {
        [TestCase("Some content without keyword", "xxx", ExpectedResult = false)]
        [TestCase("Some content with the keyword", "keyword", ExpectedResult = true)]
        public bool IsStringInWebsiteContent_ShouldReturnTrueIfKeywordInWebsiteContent(string websiteContent, string keyword)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(websiteContent),
                });
            
            var services = new ServiceCollection();
            services.AddServices();
            services.AddScoped(provider => handlerMock.Object);
            
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<IWebsiteContentChecker>();

            return service.IsStringInWebsiteContent(new Uri("http://some-web"), keyword).Result;
        }
    }
}
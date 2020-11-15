using System;
using Microsoft.Extensions.DependencyInjection;
using WebsiteChangeNotifier.Interfaces;

namespace WebsiteChangeNotifier.Services
{
    public static class DependencyInjector
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWebsiteContentChecker, WebsiteContentChecker>();
            services.AddScoped<IEmailSender, EmailSender>();
            
            return services;
        }
    }
}
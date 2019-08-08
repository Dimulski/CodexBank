﻿using CodexBank.Common.EmailSender;
using CodexBank.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace CodexBank.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(IService));
            AddAssemblyServices(services, assembly);

            return services;
        }

        public static IServiceCollection AddCommonProjectServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(IEmailSender));
            AddAssemblyServices(services, assembly);

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            AddAssemblyServices(services, assembly, true);

            return services;
        }

        private static void AddAssemblyServices(IServiceCollection services, Assembly assembly, bool isApp = false)
        {
            var servicesToRegister = assembly
                .GetTypes()
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.GetInterfaces()
                                .Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList();

            if (isApp)
            {
                servicesToRegister.ForEach(s => services.AddTransient(s.Interface, s.Implementation));
            }
            else
            {
                servicesToRegister.ForEach(s => services.AddScoped(s.Interface, s.Implementation));
            }
        }
    }
}

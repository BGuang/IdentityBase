// Copyright (c) Russlan Akiev. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityBase
{
    using ServiceBase.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ServiceBase.Notification.Email;

    public static partial class IServiceCollectionExtensions
    {
        public static void AddDefaultEmailService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IEmailService, DefaultEmailService>();

            services.AddFactory<
                DefaultEmailServiceOptions,
                DefaultEmailServiceOptionsFactory>(
                    ServiceLifetime.Scoped,
                    ServiceLifetime.Singleton); 
        }
    }
}

﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Compilation;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages.Internal;
using Microsoft.AspNetCore.Mvc.RazorPages.ModelBinding;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RazorPagesMvcCoreBuilderExtensions
    {
        public static IMvcCoreBuilder AddRazorPages(this IMvcCoreBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            RegisterFeatures(builder.PartManager);
            RegisterServices(builder.Services);

            return builder;
        }

        public static IMvcCoreBuilder AddRazorPages(this IMvcCoreBuilder builder, Action<RazorPagesOptions> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            RegisterFeatures(builder.PartManager);
            RegisterServices(builder.Services);
            builder.Services.Configure(setupAction);

            return builder;
        }

        private static void RegisterFeatures(ApplicationPartManager partManager)
        {
            partManager.FeatureProviders.Add(new MetadataReferenceFeatureProvider());
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IActionDescriptorProvider, PageActionDescriptorProvider>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IActionInvokerProvider, PageActionInvokerProvider>());

            services.TryAddSingleton<IPageFactory, DefaultPageFactory>();
            services.TryAddSingleton<IPageActivator, DefaultPageActivator>();

            services.TryAddSingleton<IPageFileProviderAccessor, DefaultPageFileProviderAccessor>();

            services.TryAddSingleton<IPageCompilationService, DefaultPageCompilationService>();
            services.TryAddSingleton<PageRazorEngineHost>();
            services.TryAddSingleton<ReferenceManager, ApplicationPartManagerReferenceManager>();
            services.TryAddSingleton<CSharpCompilationFactory, DefaultCSharpCompilationFactory>();

            services.Replace(ServiceDescriptor.Singleton<IRazorPageActivator, HackedRazorPageActivator>()); // Awful Hack

            services.TryAddSingleton<IPageArgumentBinder, DefaultPageArgumentBinder>();

            services.TryAddSingleton<PageResultExecutor>();

            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<RazorPagesOptions>, DefaultRazorPagesOptionsSetup>());
        }
    }
}

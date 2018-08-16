using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using BitcoinApi.Business;
using BitcoinApi.Data;
using BitcoinApi.Shared;

using FluentScheduler;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitcoinApi.WebApi
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public sealed class Startup
    {
        public Startup([NotNull] IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }

        [NotNull]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddControllersAsServices();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            Container.Init(builder, new DataModule(), new BusinessModule(), new WebApiModule());

            var registry = new Registry();
            registry.Schedule(() =>
                {
                    var job = Container.Current.Resolve<IWalletInfoUpdateJob>();
                    job.DoJob();
                })
                .NonReentrant()
                .ToRunEvery(5)
                .Seconds();
            JobManager.Initialize(registry);

            return new AutofacServiceProvider(Container.Current);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
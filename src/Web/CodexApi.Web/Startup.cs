using System;
using AutoMapper;
using CodexApi.Data;
using CodexApi.Services.Contracts;
using CodexApi.Services.Implementations;
using CodexBank.Common.AutoMapping.Profiles;
using CodexBank.Common.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodexApi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CodexApiDbContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IBankService, BankService>();

            services.Configure<CodexApiConfiguration>(
                this.Configuration.GetSection(nameof(CodexApiConfiguration)));

            services.PostConfigure<CodexApiConfiguration>(settings =>
            {
                if (!ValidationUtil.IsObjectValid(settings))
                {
                    throw new ApplicationException("CodexApiConfiguration is invalid");
                }
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Mapper.Initialize(config => config.AddProfile<DefaultProfile>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.SeedData();
        }
    }
}

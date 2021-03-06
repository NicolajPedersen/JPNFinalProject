﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using JPNFinalProject.Data;
using JPNFinalProject.Models;
using JPNFinalProject.Services;

using JPNFinalProject.Data.DatabaseModels;

namespace JPNFinalProject
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=dev-db02\sql2016;Initial Catalog=JPNFinalProject;User ID=JPNFinalProject;Password=JPNFinalProject2016;"));

            services.AddIdentity<ApplicationUser, IdentityRole>(x =>
            {
                x.Password.RequiredLength = 4;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<JPNFinalProjectContext>(options =>
              options.UseSqlServer(@"Server=dev-db02\sql2016;Initial Catalog=JPNFinalProject;User ID=JPNFinalProject;Password=JPNFinalProject2016;"));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<JPNFinalProjectContext>()
            //    .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            services.AddSignalR();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //var connection = @"Source=dev-db02\sql2016;Initial Catalog=JPNFinalProject;User ID=JPNFinalProject;Password=JPNFinalProject2016;";
            //services.AddDbContext<JPNFinalProjectContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseWebSockets();
            app.UseSignalR();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseSession();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "maincategoryRoute",
                    template: "{controller}/{action}/{mainCategory}");
                routes.MapRoute(
                    name: "subcategoryRoute",
                    template: "{controller}/{action}/{mainCategory}/{subCategory}/{subsubCateogry}");
                routes.MapRoute(
                    name: "categoryRoute",
                    template: "{controller}/{action}/{mainCategory}/{subCategory}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

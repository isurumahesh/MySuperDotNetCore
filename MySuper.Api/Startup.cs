using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using MySuper.Api.DatabaseContext;
using MySuper.Api.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MySuper.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static readonly LoggerFactory MyConsoleLoggerFactory
          = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true) });

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling
                    = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<MySuperContext>(options =>
            {
                options.UseLoggerFactory(MyConsoleLoggerFactory).UseSqlServer(Configuration.GetConnectionString("MySuperDbConnection"));
               
            });

            services.AddScoped<CutomerRepository>();
            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
           
        }
    }
}

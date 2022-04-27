using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ZooAPI.Profiles;
using System.Reflection;
using Data.Models;
using System.Configuration;

namespace ZooAPI
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
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZooAPI", Version = "v1" });
            });

            services.AddDbContext<Kbh_zooContext>(
            options =>
            {
                services.AddDbContext<Kbh_zooContext>(
                options => options.UseSqlServer(
                    ConfigurationManager.ConnectionStrings["ZooDB"].ConnectionString,
                providerOptions => providerOptions.EnableRetryOnFailure()));
            });

            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventTimeService, EventTimeService>();
            services.AddScoped<IQRCodeService, QRCodeService>();
            services.AddScoped<IAzureStorageService, AzureStorageService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddAutoMapper(typeof(AnimalProfile), typeof(EventProfile), typeof(EventTimestampProfile), typeof(FeedbackProfile), typeof(CategoryProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZooAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            //Azure AD
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

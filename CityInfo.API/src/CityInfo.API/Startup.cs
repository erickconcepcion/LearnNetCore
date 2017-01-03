using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using CityInfo.API.Services;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",optional:false, reloadOnChange:true);
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o=>o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            var connectionstring = Configuration["Connections:ConnectionString"];
            services.AddDbContext<CityInfoDbContext>(o=>o.UseSqlServer(connectionstring));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            CityInfoDbContext context)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.EnsureSeedDataForContext();

            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize(map => {
                map.CreateMap<Entities.City, Models.CityWithoutpointsOfInterestDTO>();
                map.CreateMap<Entities.City,Models.CityDTO>();
                map.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDTO>();
                map.CreateMap<Models.PointOfInterestCreate, Entities.PointOfInterest>();
                map.CreateMap<Models.PointOfInterestUpdate, Entities.PointOfInterest>();
                map.CreateMap<Entities.PointOfInterest, Models.PointOfInterestUpdate>();
            });

            app.UseMvc();

            
            
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(Configuration["TestInfos:Gretings"]);
            //});
        }
    }
}

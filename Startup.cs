using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoviesDIOApi.Middleware;
using MoviesDIOApi.Repository;
using MoviesDIOApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi
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
            services.AddScoped<IRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            services.Configure<JsonOptions>(o =>
            {
                o.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddSingleton<ExceptionMiddleware>();
            services.AddSwaggerGen();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                    x.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

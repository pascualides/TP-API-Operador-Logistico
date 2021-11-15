using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using NSwag.AspNetCore;
using operadorLogisticoAPI.API;
using operadorLogisticoAPI.Repositories.Contexts;
using Microsoft.OpenApi.Models;


namespace operadorLogisticoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OperadorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-282k6-68.us.auth0.com/";
                options.Audience = "https://www.example.com/api-logistic-operator-norte";
            });
            ///Config Autorizacion
            services.AddAuthorization(options =>
            {
                options.AddPolicy("write:envios", policy => policy.Requirements.Add(new HasScopeRequirement("write:envios", "https://dev-282k6-68.us.auth0.com/")));
                options.AddPolicy("write:estados_envios", policy => policy.Requirements.Add(new HasScopeRequirement("write:estados_envios", "https://dev-282k6-68.us.auth0.com/")));
                options.AddPolicy("write:repartidores", policy => policy.Requirements.Add(new HasScopeRequirement("write:repartidores", "https://dev-282k6-68.us.auth0.com/")));

            });

            services.AddControllers();

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AWS Serverless Asp.Net Core Web API", Version = "v1" });
            });


            //services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication(); // SACADO DEL EJEMPLO

            app.UseAuthorization();

            //app.UseOpenApi();
            //app.UseSwaggerUi3();

            //c =>
            //{
            //    c.SwaggerRoutes.Add(new SwaggerUi3Route("OperadorLogistico", "/swagger/v1/swagger.json"));
            //});

            app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/Prod/swagger/v1/swagger.json","AWS Serverless Asp.Net Core Web API");
                    c.RoutePrefix = "swagger";
                });



            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}

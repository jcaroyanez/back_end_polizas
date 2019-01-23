using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;
using Prueba_Crud.Models;

namespace Prueba_Crud
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<dbContext>(options => options.UseMySql(Configuration.GetConnectionString("Database")));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "https://github.com/jcaroyanez",
                    ValidIssuer = "https://github.com/jcaroyanez",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fggfasdfaVSDFNFGNDFVASDJKFHEUIFUIgfuiasdgfasgfasdui"))
                };
            });//

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, swaggerSettings =>
            {
                swaggerSettings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("custom-auth"));

                swaggerSettings.GeneratorSettings.DocumentProcessors.Add(
                    new SecurityDefinitionAppender("custom-auth", new SwaggerSecurityScheme
                    {
                        Type = SwaggerSecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Description = "The language of the response",
                        In = SwaggerSecurityApiKeyLocation.Header
                    }));

                swaggerSettings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Api Rest Inicial";
                    document.Info.Description = "Plantilla inicial de api";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new SwaggerContact
                    {
                        Name = "Jose luis caro yanez",
                        Email = "jcaroyanez@gmail.com",
                        Url = "https://github.com/jcaroyanez"
                    };
                    document.Info.License = new SwaggerLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });


            app.UseCors("AllowAllHeaders");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

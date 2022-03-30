using Azure.Storage.Blobs;
using CosmosDb.DAL;
using CosmosDb.DAL.Abstraction;
using CosmosDb.DAL.Implementation;
using CosmosDb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDb
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
           // services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title ="CosmosDb", Version = "v1" });
            });
            services.AddScoped<ICosmosConnection, CosmosConnection>();
            services.AddScoped<ICosmosDataAdapter, CosmosDataAdapter>();
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin().
                   
                    AllowAnyHeader().AllowAnyMethod();



                });
            });
            services.AddScoped(options =>
            {
                return new BlobServiceClient(Configuration.GetConnectionString("AzureStorage"));
            });
            services.AddScoped<IFileManagerLogic, FileManagerLogic>();
            services.AddScoped<IAuth, Auth>();
            var appsettingSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appsettingSection);

            var appSettings = appsettingSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Key);
            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                jwt =>
                {
                    jwt.RequireHttpsMetadata = false;
                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("EnableCors");

           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosmosDb");
            }
                );
        }
    }
}

using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ToDoListApi.Data;
using ToDoListApi.Entities;
using ToDoListApi.Extensions;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Repositories;
using ToDoListApi.Services;

[assembly: ApiController]
namespace ToDoListApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Constants.ToDoDbConnectionString)));


            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

           
            
            services.AddDbContext<UserStoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Constants.UserStoreConnectionString)));
            services.AddIdentityCore<AppUser>()
                .AddEntityFrameworkStores<UserStoreDbContext>();
            
            services.Configure<TokenManagement>(Configuration.GetSection(Constants.TokenManagement));
            var token = Configuration.GetSection(Constants.TokenManagement).Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
        }

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

            app.UseCustomExceptionHandler();
            app.UseCors(config =>
            {
                config.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials();
            });
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
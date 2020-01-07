using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ToDoListApi.Data;
using ToDoListApi.Email;
using ToDoListApi.Entities;
using ToDoListApi.Extensions;
using ToDoListApi.Helpers;
using ToDoListApi.Options;
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
                options.UseNpgsql(Environment.GetEnvironmentVariable(Constants.ToDoDbConnectionString)));
            services.AddDbContext<TokenStoreDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable(Constants.TokenStoreDb)));

            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<UserStoreDbContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable(Constants.UserStoreConnectionString)));
            services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<UserStoreDbContext>()
                .AddDefaultTokenProviders();
   
            
            services.Configure<JwtSettings>(Configuration.GetSection(Constants.JwtSettings));
            services.Configure<EmailVerificationSettings>(
                Configuration.GetSection(Constants.EmailVerificationSettings));
            var token = Configuration.GetSection(Constants.JwtSettings).Get<JwtSettings>();
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
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IRefreshTokenHandler, RefreshTokenHandler>();
            services.AddTransient<IEmailSender, EmailSender>();
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
                app.UseDeveloperExceptionPage();

//                app.UseCustomExceptionHandler();

            }
            
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
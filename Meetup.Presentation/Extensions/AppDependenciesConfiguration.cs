using FluentValidation;
using FluentValidation.AspNetCore;
using Meetup.BusinessLogic.Accounts.Validations;
using Meetup.BusinessLogic.Interfaces;
using Meetup.BusinessLogic.RepositoriesServices;
using Meetup.DataAccess.DataContext;
using Meetup.DataAccess.Interfaces;
using Meetup.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Meetup.Presentation.Extensions
{
    public static class AppDependenciesConfiguration
    {
        public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder,IConfiguration configuration)
        {

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JwtBearerDefaults Authorisation header using Bearer sscheme. \r\n\r\n
                        Enter 'Bearer' [space] and then your taken in the text input below.
                        \r\n\r\n Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
               {
                 {
                      new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                  new List<string>()
                 }

             });

        });


            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            

            builder.Services
                .AddValidatorsFromAssemblyContaining<UserValidator>()
                .AddFluentValidationAutoValidation();
 
            builder.Services.AddDbContext<MeetupContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<IUserRepository,UserRepoository>()
                .AddScoped<IEventRepository, EventRepository>()
                .AddScoped<IBookedEventRepository, BookedEventRepository>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IEventService, EventService>()
                .AddScoped<IBookedEventService, BookedEventService>();
               return builder.Services;

        }

        public static void Configure(this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

    }
}

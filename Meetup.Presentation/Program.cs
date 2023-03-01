using FluentValidation.AspNetCore;
using Meetup.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
 
builder.Services.AddEndpointsApiExplorer();

builder.ConfigureServices(builder.Configuration);
var app = builder.Build();
 
app.Configure();



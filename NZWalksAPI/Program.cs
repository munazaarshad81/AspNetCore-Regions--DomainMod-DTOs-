using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Data;
using NZWalksAPI.Mappings;
using NZWalksAPI.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"))); //Adding dependency injection
                                                                                                 //
builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"))); //Adding dependency injection

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>(); //Adding dependency injection for SQL region repository
// builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>(); //Adding dependency injection for InMemory region repository
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>(); //Adding dependency injection for SQL Walk repository

// builder.Services.AddAutoMapper(typeof(AutoMappingProfiles));


builder.Services.AddAutoMapper(typeof(AutoMappingProfiles));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


var app = builder.Build();

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

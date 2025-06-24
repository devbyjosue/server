using Microsoft.EntityFrameworkCore;
using Server.Data;
using UserApi.Services;
using UserApi.Services.Interfaces;
using RolesApi.Services;
using RolesApi.Services.Interfaces;
using MenuApi.Services;
using MenuApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Negotiate; // Import the Negotiate namespace
using Microsoft.AspNetCore.Authorization; // Import the Authorization namespace
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRolesServices, RolesServices>();
builder.Services.AddScoped<IMenuServices, MenuServices>();

builder.Services.AddDbContext<ServerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddHttpContextAccessor();
// builder.Services.AddHttpsRedirection(options =>
// {
//     options.HttpsPort = 7216; 
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

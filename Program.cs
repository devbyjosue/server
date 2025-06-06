using Microsoft.EntityFrameworkCore;
using Server.Data;
using UserApi.Services;
using UserApi.Services.Interfaces;
using RolesApi.Services;
using RolesApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {     
options.AddPolicy("AllowSpecificOrigins", policy =>     {  
       policy.WithOrigins("http://localhost:4200")   
        .AllowAnyHeader()       
        .AllowAnyMethod();   
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

builder.Services.AddDbContext<ServerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(
    options => options.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();

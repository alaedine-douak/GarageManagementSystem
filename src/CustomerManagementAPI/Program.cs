using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using CustomerManagementAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetConnectionString("CustomerManagementCN");

builder.Services.AddDbContext<CustomerManagementDbContext>(opts => 
    opts.UseSqlServer(sqlConnectionString));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(x => 
{
    x.SwaggerDoc("v1", new OpenApiInfo{ Title = "CustomerManagement API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSwagger();

app.UseSwaggerUI(x => 
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManagement API - v1");
});

app.MapControllers();

app.Run();

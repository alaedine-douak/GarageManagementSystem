using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(x => 
{
    x.SwaggerDoc("v1", new OpenApiInfo{ Title = "VehicleManagement API", Version = "v1" });
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
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleManagement API - v1");
});

app.MapControllers();

app.Run();

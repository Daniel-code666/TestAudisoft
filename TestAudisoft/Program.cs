using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TestAudisoft.Application;
using TestAudisoft.Extensions;
using TestAudisoft.Infrastructure;
using TestAudisoft.Infrastructure.Persistence;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TestAudisoft API",
        Version = "v1"
    });

    var xml_file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xml_path = Path.Combine(AppContext.BaseDirectory, xml_file);

    options.IncludeXmlComments(xml_path);
});

builder.Services.AddDbContext<TestAudisoftDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TestAudisoftDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

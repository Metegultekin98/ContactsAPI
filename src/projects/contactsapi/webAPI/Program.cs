using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Application.Pipelines.Security;
using Core.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerUI;
using webAPI.Application;
using webAPI.Persistence.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecryptedJsonModelBinderProvider());
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepositoryModule()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", new CorsPolicyBuilder()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .Build());
});

var app = builder.Build();

app.UseStaticFiles();

app.UseSwagger(x =>
{
    x.SerializeAsV2 = true;
});
app.UseSwaggerUI(options =>
{
    options.DocExpansion(DocExpansion.None);
    options.DefaultModelExpandDepth(-1);
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    options.InjectStylesheet("/swagger-custom/swagger-custom-styles.css");
    options.InjectJavascript("/swagger-custom/swagger-custom-script.js", "text/javascript");
});

app.UseCors("AllowAll");
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
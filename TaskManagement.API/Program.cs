using System.Text.Json.Serialization;
using TaskManagement.API.Extensions;
using TaskManagement.API.Filter;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors();
        builder.Services.AddControllers();


        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagement.API", Version = "v1" });
            c.OperationFilter<AddHeaderOperationFilter>();
        });

        builder.Services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()))
                        .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never);

        builder.Services
            .AddObject(builder.Configuration)
            .AddContext(builder.Configuration)
            .AddServices()
            .AddRepositories()
            .AddAutoMapper()
            .AddCors()
            .AddResponseCompression();


        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        if (app.Environment.IsProduction())
            app.UseHttpsRedirection();

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.MapControllers();

        app.Run();
    }
}
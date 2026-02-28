using CrewSchedule.Infrastructure.Persistence;
using CrewSchedule.Infrastructure;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;
using Microsoft.EntityFrameworkCore;
using MediatR;
using CrewSchedule.Application.Behaviors;
using CrewSchedule.WebApi.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using FluentValidation;
using CrewSchedule.Domain.Policies;
using CrewSchedule.Application;
using CrewSchedule.Application.Commands;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((ctx, services, cfg) => cfg
            .WriteTo.Console()
            .WriteTo.File("logs/crew-log.txt", rollingInterval: RollingInterval.Day));

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddScoped<IRestPolicy, StandardRestPolicy>();
        builder.Services.AddScoped<ICrewCompositionPolicy, StandardCrewCompositionPolicy>();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}
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
using CrewSchedule.Application.Commands;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((ctx, services, cfg) => cfg
            .WriteTo.Console()
            .WriteTo.File("logs/crew-log.txt", rollingInterval: RollingInterval.Day));

     
        builder.Services.AddDbContext<CrewDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CrewConnection")));

        builder.Services.AddDbContext<FlightHoursDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("FlightHoursConnection")));

        
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        builder.Services.AddScoped<IRestPolicy, StandardRestPolicy>();
        builder.Services.AddScoped<ICrewCompositionPolicy, StandardCrewCompositionPolicy>();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapPost("/flights/assign",
            async (AssignFullCrewToFlightCommand command,
                    IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.Ok();
            })
            .WithName("AssignCrewToFlight");

        app.Run();
    }
}
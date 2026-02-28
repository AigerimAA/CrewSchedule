using CrewSchedule.Application.Repositories;
using CrewSchedule.Infrastructure.Persistence.Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Infrastructure.Persistence;
using CrewSchedule.Infrastructure.Services;

namespace CrewSchedule.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CrewDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("CrewConnection")));

            services.AddDbContext<FlightHoursDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("FlightHoursConnection"))); 

            services.AddScoped<IDbConnection>(_ =>
                    new SqlConnection(
                        configuration.GetConnectionString("CrewConnection")));

            services.AddScoped<IFlightReadRepository, FlightReadRepository>();

            services.AddScoped<ISwapReadRepository, SwapReadRepository>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}

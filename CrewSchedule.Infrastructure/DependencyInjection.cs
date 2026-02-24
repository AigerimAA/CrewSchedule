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

namespace CrewSchedule.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(_ =>
                    new SqlConnection(
                        configuration.GetConnectionString("CrewConnection")));
            services.AddScoped<IFlightReadRepository, FlightReadRepository>();

            services.AddDbContext<CrewDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("CrewConnection")));

            return services;
        }
    }
}

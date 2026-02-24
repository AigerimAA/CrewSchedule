using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Application.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence.Dapper
{
    public class FlightReadRepository : IFlightReadRepository
    {
        private readonly IDbConnection _connection;

        public FlightReadRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<FlightDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            const string sql = @"
            SELECT 
                Id,
                FlightNumber,
                DepartureAirport,
                ArrivalAirport,
                DepartureTimeUtc,
                ArrivalTimeUtc
            FROM Flights";

            var result = await _connection.QueryAsync<FlightDto>(sql);
            return result.ToList();
        }
        public async Task<FlightDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            const string sql = @"
            SELECT *
            FROM Flights
            WHERE Id = @Id";

            return await _connection.QueryFirstOrDefaultAsync<FlightDto>(sql, new { Id = id });
        }
        public async Task<List<CrewMemberDto>> GetCrewForFlightAsync(Guid flightId, CancellationToken cancellationToken)
        {
            const string sql = @"
            SELECT 
                cm.Id,
                cm.FullName,
                cm.Position
            FROM Assignments a
            INNER JOIN CrewMembers cm 
                ON cm.Id = a.CrewMemberId
            WHERE a.FlightId = @FlightId";

            var result = await _connection.QueryAsync<CrewMemberDto>(sql, new { FlightId = flightId });

            return result.ToList();
        }
    }
}

using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CrewSchedule.Infrastructure.Persistence.Dapper
{
    public class SwapReadRepository : ISwapReadRepository
    {
        private readonly IDbConnection _connection;

        public SwapReadRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<SwapRequestDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    Id,
                    FromCrewMemberId,
                    ToCrewMemberId,
                    FlightId,
                    Status,
                    CreatedAtUtc
                FROM SwapRequests
                WHERE Id = @Id";

            return await _connection.QueryFirstOrDefaultAsync<SwapRequestDto>(sql, new {Id = id});
        }

        public async Task<List<SwapRequestDto>> GetByCrewMemberAsync(Guid crewMemberId, CancellationToken cancellationToken)
        {
            const string sql = @"
                SELECT
                    Id,
                    FromCrewMemberId,
                    ToCrewMemberId,
                    FlightId,
                    Status,
                    CreatedAtUtc
                FROM SwapRequests
                WHERE FromCrewMemberId = @CrewMemberId
                   OR ToCrewMemberId   = @CrewMemberId
                ORDER BY CreatedAtUtc DESC";

            var result = await _connection.QueryAsync<SwapRequestDto>(sql, new { CrewMemberId = crewMemberId});
            return result.ToList();
        }
    }
}

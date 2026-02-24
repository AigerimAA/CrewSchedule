using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CrewSchedule.Application.Queries
{
    public record class GetSwapRequestQuery(Guid Id) : IRequest<SwapRequestDto>;
    
    public class GetSwapRequestHandler : IRequestHandler<GetSwapRequestQuery, SwapRequestDto>
    {
        private readonly ICrewDbContext _context;
        private readonly IMapper _mapper;

        public GetSwapRequestHandler(ICrewDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SwapRequestDto> Handle(GetSwapRequestQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SwapRequests
                .AsNoTracking()
                .FirstAsync(x => x.Id == request.Id, cancellationToken);

            return _mapper.Map<SwapRequestDto>(entity);
        }
    }

}

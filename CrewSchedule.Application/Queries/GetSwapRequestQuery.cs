using CrewSchedule.Application.DTO;
using CrewSchedule.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CrewSchedule.Application.Exceptions;
using CrewSchedule.Domain.Aggregates;

namespace CrewSchedule.Application.Queries
{
    public record class GetSwapRequestQuery(Guid Id) : IRequest<SwapRequestDto>;
    
    public class GetSwapRequestHandler : IRequestHandler<GetSwapRequestQuery, SwapRequestDto>
    {
        private readonly ISwapReadRepository _repository;

        public GetSwapRequestHandler(ISwapReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<SwapRequestDto> Handle(GetSwapRequestQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (dto is null)
                throw new NotFoundException(nameof(SwapRequest), request.Id);

            return dto;
        }
    }

}

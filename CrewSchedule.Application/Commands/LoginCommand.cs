using CrewSchedule.Application.Exceptions;
using CrewSchedule.Application.Interfaces;
using CrewSchedule.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CrewSchedule.Application.Commands
{
    public record LoginCommand(string FullName) : IRequest<string>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ICrewDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(ICrewDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var crewMember = await _context.CrewMembers
                .FirstOrDefaultAsync(x => x.FullName == request.FullName, cancellationToken);

            if (crewMember is null)
                throw new NotFoundException(nameof(CrewMember), request.FullName);

            return _tokenService.GenerateToken(crewMember);
        }
    }
}

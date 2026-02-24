using CrewSchedule.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CrewSchedule.Application.DTO;

namespace CrewSchedule.Application.Mappings
{
    public class SwapProfiles : Profile
    {
        public SwapProfiles()
        {
            CreateMap<SwapRequest, SwapRequestDto>();
        }
    }
}

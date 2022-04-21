using AutoMapper;
using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooAPI.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, RespEventDTO>()
                .ForMember(d => d.Animals, o => o.MapFrom(s => s.AnimalHasEvents.Select(x => x.AnimalIdAnimalNavigation).ToList()))
                .ForMember(d => d.Schedules, o => o.MapFrom(s => s.EventTimes));

        }
    }
}

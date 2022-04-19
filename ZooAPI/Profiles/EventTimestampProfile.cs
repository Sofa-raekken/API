using AutoMapper;
using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooAPI.Profiles
{
    public class EventTimestampProfile : Profile
    {
        public EventTimestampProfile()
        {
            CreateMap<EventTime, RespEventTimestampDTO>()
                .ForMember(d => d.EventId, o => o.MapFrom(s => s.FkIdEvent))
                .ForMember(d => d.EventName, o => o.MapFrom(s => s.FkIdEventNavigation.Name))
                .ForMember(d => d.OccurringDate, o => o.MapFrom(s => s.Date));

            CreateMap<CreateEventTimestampDTO, EventTime>()
                .ForMember(d => d.FkIdEvent, o => o.MapFrom(s => s.EventId))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.OccurringDate));
        }
    }
}

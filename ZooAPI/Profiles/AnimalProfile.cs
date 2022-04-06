using AutoMapper;
using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooAPI.Profiles
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<Diet, DietDTO>();
            CreateMap<Event, EventDTO>();
            CreateMap<DietDTO, Diet>();
            CreateMap<EventDTO, Event>();
            CreateMap<Animal, AnimalDTO>()
                .ForMember(d => d.Events, o => o.MapFrom(s => s.AnimalHasEvents.Select(x => x.EventIdEventNavigation).ToList()))
                .ForMember(d => d.Diets, o => o.MapFrom(s => s.AnimalHasDiets.Select(x => x.DietIdDietNavigation).ToList()));

            //CreateMap<DietDTO,AnimalHasDiet>()
            //    .ForMember(d => d.DietIdDietNavigation, o => o.MapFrom(s => s));

            CreateMap<CreateAnimalDTO, Animal>();

            CreateMap<Animal, AnimalShortInfoDTO > ();
        }
    }
}

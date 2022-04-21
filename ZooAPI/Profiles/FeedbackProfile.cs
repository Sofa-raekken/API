using AutoMapper;
using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooAPI.Profiles
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, RespFeedbackDTO>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.CategoryIdCategoryNavigation.CategoryName))
                .ForMember(d => d.Resolved, o => o.MapFrom(s => s.Resolved.Value == 1 ? true : false));

            CreateMap<CreateFeedbackDTO, Feedback>()
                .ForMember(d => d.CategoryIdCategory, o => o.MapFrom(s => s.CategoryId));
        }
    }
}

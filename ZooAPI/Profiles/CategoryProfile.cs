using AutoMapper;
using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooAPI.Profiles
{
    public class CategoryProfile : Profile
    {

        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}

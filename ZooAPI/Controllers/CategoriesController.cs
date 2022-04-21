using AutoMapper;
using Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooAPI.ADRoles;

namespace ZooAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        public IMapper Mapper { get; }
        public ICategoryService CategoryService { get; }

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            CategoryService = categoryService;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            try
            {
                return Ok(Mapper.Map<List<CategoryDTO>>(await CategoryService.GetAllCategories()));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}

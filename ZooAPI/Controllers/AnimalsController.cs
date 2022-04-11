using AutoMapper;
using Data.DTO;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooAPI.ADRoles;

namespace ZooAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : Controller
    {
        public IAnimalService AnimalService { get; }
        public IMapper Mapper { get; }
        public AnimalsController(IAnimalService animalService, IMapper mapper)
        {
            AnimalService = animalService;
            Mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public ActionResult<AnimalDTO> GetAnimal(int id)
        {
            Animal animal = AnimalService.GetAnimal(id);

            if (animal is not null)
            {
                AnimalDTO animalDTO = Mapper.Map<AnimalDTO>(animal);
                return Ok(animalDTO);
            }
            else
            {
                return BadRequest("No animal found");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<AnimalDTO>>> GetAnimalsWithoutDisabled()
        {
            return Ok(Mapper.Map<List<AnimalDTO>>(await AnimalService.GetAnimals(false)));
        }

        [HttpGet]
        [Authorize(Roles = Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
        [Route("alsodisabled")]
        public async Task<ActionResult<List<AnimalDTO>>> GetAnimalsWithDisabled()
        {
            return Ok(Mapper.Map<List<AnimalDTO>>(await AnimalService.GetAnimals(true)));
        }

        [HttpPost]
        [Authorize(Roles = Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
        public async Task<ActionResult<Animal>> PostAnimal([FromBody] CreateAnimalDTO animal)
        {
            try
            {
                int animalId = await AnimalService.InsertAnimal(Mapper.Map<Animal>(animal), animal.Diets);
                if (animalId > 0)
                {
                    return Ok(Mapper.Map<AnimalDTO>(AnimalService.GetAnimal(animalId)));
                }
                else
                {
                    return StatusCode(500);

                }
            }
            catch (Exception)
            {

                return StatusCode(500, animal);
            }

        }

        [HttpDelete]
        [Authorize(Roles = Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAnimal(int id)
        {
            if (await AnimalService.DeleteAnimal(id))
            {
                return Ok("Successfully Deleted");
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }

        [HttpPut]
        [Authorize(Roles = Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
        [Route("{id}")]
        public async Task<ActionResult<Animal>> FullUpdateAnimal(int id,[FromBody] UpdateAnimalDTO animal)
        {
            try
            {
                Animal animalModel = await AnimalService.FullUpdateAnimal(id, animal);
                if (animalModel != null)
                {
                    return Ok(Mapper.Map<AnimalDTO>(animalModel));
                }
                else
                {
                    return BadRequest(animal);
                }
            }
            catch (Exception)
            {

                return StatusCode(500, animal);
            }
        }
    }
}

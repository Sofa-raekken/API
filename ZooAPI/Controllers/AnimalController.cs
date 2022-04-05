using Data.DTO;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
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
    public class AnimalController : Controller
    {
        public IAnimalService AnimalService { get; set; }
        public AnimalController(IAnimalService animalService)
        {
            AnimalService = animalService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            Animal animal = await AnimalService.GetAnimal(id);
            if(animal is not null)
            {
                return Ok(animal);
            }
            else
            {
                return BadRequest("No animal found");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]s")]
        public async Task<ActionResult<List<Animal>>> GetAnimals()
        {
            return Ok(await AnimalService.GetAnimals());
        }

        [HttpPost]
        [Authorize(Roles = Roles.UserRole)]
        [RequiredScope(ADScopes.scopeRequiredByApi)]
        [Route("[controller]")]
        public async Task<ActionResult<Animal>> PostAnimal([FromBody] CreateAnimalDTO animal)
        {
            return Ok(await AnimalService.InsertAnimal(animal));
        }

        [HttpDelete]
        [Authorize(Roles = Roles.UserRole)]
        [RequiredScope(ADScopes.scopeRequiredByApi)]
        [Route("[controller]/{id}")]
        public async Task<ActionResult> DeleteAnimal(int id)
        {
            if (await AnimalService.DeleteAnimal(id))
            {
                return Ok("Successfully Created");
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }

        [HttpPut]
        [Authorize(Roles = Roles.UserRole)]
        [RequiredScope(ADScopes.scopeRequiredByApi)]
        [Route("[controller]/{id}")]
        public async Task<ActionResult<Animal>> FullUpdateAnimal([FromBody]UpdateAnimalDTO animal)
        {
            Animal animalModel = await AnimalService.FullUpdateAnimal(animal);
            if (animalModel != null)
            {
                return Ok(animalModel);
            }
            else
            {
                return StatusCode(500, animal);
            }

        }
    }
}

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
    [Authorize(Roles = Role.UserRole)]
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : Controller
    {
        public IAnimalService AnimalService { get; }
        public IQRCodeService QrCodeService { get; }
        public IAzureStorageService AzureStorageService { get; }
        public IMapper Mapper { get; }
        public AnimalsController(IAnimalService animalService, IMapper mapper, IQRCodeService qRCodeService, IAzureStorageService azureStorageService)
        {
            AnimalService = animalService;
            QrCodeService = qRCodeService;
            AzureStorageService = azureStorageService;
            Mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public ActionResult<AnimalDTO> GetAnimal(int id)
        {
            try
            {
                Animal animal = AnimalService.GetAnimal(id);

                return Ok(Mapper.Map<AnimalDTO>(animal));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<AnimalDTO>>> GetAnimalsWithoutDisabled()
        {
            try
            {
                return Ok(Mapper.Map<List<AnimalDTO>>(await AnimalService.GetAnimals(false)));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("alsodisabled")]
        public async Task<ActionResult<List<AnimalDTO>>> GetAnimalsWithDisabled()
        {
            try
            {
                return Ok(Mapper.Map<List<AnimalDTO>>(await AnimalService.GetAnimals(true)));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> InsertAnimal([FromBody] CreateAnimalDTO animal)
        {
            try
            {
                int animalId = await AnimalService.InsertAnimal(Mapper.Map<Animal>(animal), animal.Diets);
                if (animalId > 0)
                {
                    string filePath = QrCodeService.GenerateQRCode("" + animalId);
                    string azureFilePath = AzureStorageService.SendFileToAzureStorage(animalId, filePath);

                    if (await AnimalService.UpdateQRCodeAnimal(animalId, azureFilePath))
                    {
                        return Ok(Mapper.Map<AnimalDTO>(AnimalService.GetAnimal(animalId)));
                    }
                }
                return BadRequest("Bad Request");

            }
            catch (Exception)
            {

                return StatusCode(500, animal);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAnimal(int id)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Animal>> FullUpdateAnimal(int id, [FromBody] UpdateAnimalDTO animal)
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

        [HttpPatch("{id}/{qrcode}")]
        public async Task<ActionResult<Animal>> UpdateQROnAnimal(int id, [FromBody] PatchQRCodeDTO patchQRCodeDTO)
        {
            try
            {
                string filePath = QrCodeService.GenerateQRCode(patchQRCodeDTO.QRCode);

                if (string.IsNullOrWhiteSpace(filePath)) { return BadRequest("Bad Request"); }

                string azureFilePath = AzureStorageService.SendFileToAzureStorage(id, filePath);

                if (string.IsNullOrWhiteSpace(azureFilePath)) { return BadRequest("Bad Request"); }

                if (await AnimalService.UpdateQRCodeAnimal(id, azureFilePath))
                {
                    return Ok(Mapper.Map<AnimalDTO>(AnimalService.GetAnimal(id)));
                }

                return BadRequest("Bad Request");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}

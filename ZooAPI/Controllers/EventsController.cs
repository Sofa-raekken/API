using AutoMapper;
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
    [Authorize(Roles = Role.UserRole)]
    [ApiController]
    [Route("[controller]")]
    public class EventsController : Controller
    {
        public IEventService EventService { get; }
        public IMapper Mapper { get; }
        public EventsController(IEventService eventService, IMapper mapper)
        {
            EventService = eventService;
            Mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<RespEventDTO>>> GetEvents()
        {
            try
            {
                return Ok(Mapper.Map<List<RespEventDTO>>(await EventService.GetEvents()));

            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RespEventDTO>> GetEvent(int id)
        {
            try
            {
                return Ok(Mapper.Map<RespEventDTO>(await EventService.GetEvent(id)));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RespEventDTO>> InsertEvent([FromBody] CreateEventDTO eventDTO)
        {
            try
            {
                Event eventModel = await EventService.InsertEvent(eventDTO);

                if (eventModel is not null)
                {
                    return Ok(Mapper.Map<RespEventDTO>(eventModel));
                }
                return null;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RespEventDTO>> DeleteEvent(int id)
        {
            try
            {
                if (await EventService.DeleteEvent(id))
                {
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RespEventDTO>> UpdateEvent(int id, [FromBody] UpdateEventDTO updateEventDTO)
        {
            try
            {
                var updatedEntity = await EventService.UpdateEvent(id, updateEventDTO);
                if (updatedEntity is not null)
                {
                    return Ok(Mapper.Map<RespEventDTO>(updatedEntity));
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}

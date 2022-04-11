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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EventsController : Controller
    {
        public IEventService EventService { get; }
        public IEventTimeService EventTimeService { get; }
        public IMapper Mapper { get; }
        public EventsController(IEventService eventService, IEventTimeService eventTimeService, IMapper mapper)
        {
            EventService = eventService;
            EventTimeService = eventTimeService;
            Mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<EventResponseDTO>>> GetEvents()
        {
            return Ok(Mapper.Map<List<EventResponseDTO>>(await EventService.GetEvents()));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<ActionResult<EventResponseDTO>> GetEvent(int id)
        {
            return Ok(Mapper.Map<EventResponseDTO>(await EventService.GetEvent(id)));
        }

        [HttpPost]
        [Authorize(Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
        public async Task<ActionResult<EventResponseDTO>> InsertEvent([FromBody] CreateEventDTO eventDTO)
        {
            try
            {
                Event eventModel = await EventService.InsertEvent(eventDTO);

                if (eventModel is not null)
                {
                    return Ok(Mapper.Map<EventResponseDTO>(eventModel));
                }
                return null;
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Authorize(Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
        [Route("{id}")]
        public async Task<ActionResult<EventResponseDTO>> DeleteEvent(int id)
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
    }
}

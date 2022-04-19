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
        public async Task<ActionResult<List<RespEventDTO>>> GetEvents()
        {
            return Ok(Mapper.Map<List<RespEventDTO>>(await EventService.GetEvents()));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RespEventDTO>> GetEvent(int id)
        {
            return Ok(Mapper.Map<RespEventDTO>(await EventService.GetEvent(id)));
        }

        [HttpPost]
        [Authorize(Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
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
        [Authorize(Role.UserRole)]
        [RequiredScope(Scope.scopeRequiredByApi)]
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
    }
}

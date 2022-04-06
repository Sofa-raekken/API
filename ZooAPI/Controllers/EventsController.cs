using AutoMapper;
using Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooAPI.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<List<EventResponseDTO>>> GetEvents()
        {
            return Ok(Mapper.Map<List<EventResponseDTO>>(await EventService.GetEvents()));
        }
    }
}

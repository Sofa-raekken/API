using AutoMapper;
using Data.DTO;
using Data.Models;
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
    [Authorize(Roles = Role.UserRole)]
    [ApiController]
    [Route("[controller]")]
    public class EventTimestampsController : Controller
    {
        public IEventTimeService EventTimeService { get; }
        public IMapper Mapper { get; }
        public EventTimestampsController(IEventTimeService eventTimeService, IMapper mapper)
        {
            EventTimeService = eventTimeService;
            Mapper = mapper;
        }

        [HttpGet("{date}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<RespEventTimestampDTO>>> GetEventTimestampsForDate(DateTime date)
        {
            try
            {
                var eventTimestamps = await EventTimeService.GetEventsForDate(date);
                return Ok(Mapper.Map<List<RespEventTimestampDTO>>(eventTimestamps));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<RespEventTimestampDTO>>> GetEventTimestamps()
        {
            try
            {
                var eventTimestamps = await EventTimeService.GetAllEventTimestamps();
                return Ok(Mapper.Map<List<RespEventTimestampDTO>>(eventTimestamps));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpDelete("{timestampId}")]
        public async Task<IActionResult> DeleteEventTimestamp(int timestampId)
        {
            try
            {
                if (await EventTimeService.DeleteEventTimestap(timestampId))
                {
                    return Ok();

                }
                return BadRequest("Bad Request");

            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertTimestamps([FromBody] CreateEventTimestampsDTO createEventTimestampsDTO)
        {
            try
            {
                if (await EventTimeService.InsertEventTimestampsForDate(Mapper.Map<List<EventTime>>(createEventTimestampsDTO.eventTimestamps)))
                {
                    return Ok();

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

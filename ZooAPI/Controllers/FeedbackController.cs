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
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = Role.UserRole)]
    public class FeedbackController : Controller
    {
        public IFeedbackService FeedbackService { get; }
        public IMapper Mapper { get; }
        public FeedbackController(IFeedbackService feedbackService, IMapper mapper)
        {
            FeedbackService = feedbackService;
            Mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<RespFeedbackDTO>>> GetFeedback([FromQuery] int count = 0, int skip = 0, DateTime? startdate = null, DateTime? enddate = null)
        {
            try
            {
                var feedback = await FeedbackService.GetFeedback(count, skip, startdate, enddate);
                return Ok(Mapper.Map<List<RespFeedbackDTO>>(feedback));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost]
        public async Task<ActionResult<RespFeedbackDTO>> InsertFeedback([FromBody] CreateFeedbackDTO feedbackDTO)
        {
            try
            {
                var feedback = await FeedbackService.InsertFeedback(Mapper.Map<Feedback>(feedbackDTO));
                if (feedback is not null)
                {
                    return Ok(Mapper.Map<RespFeedbackDTO>(feedback));
                }
                return BadRequest("Bad Request");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPut("{id}/resolve")]
        public async Task<ActionResult<RespFeedbackDTO>> ResolvedFeedback(int id, [FromBody] UpdateFeedbackResolvedDTO feedbackResolvedDTO)
        {
            try
            {
                var feedback = await FeedbackService.ResolvedFeedback(id, feedbackResolvedDTO.isResolved);
                if (feedback is not null)
                {
                    return Ok(Mapper.Map<RespFeedbackDTO>(feedback));
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

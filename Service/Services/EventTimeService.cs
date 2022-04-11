using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EventTimeService : IEventTimeService
    {
        private Kbh_zooContext Context;
        public EventTimeService(Kbh_zooContext context)
        {
            Context = context;
        }
        public async Task<List<EventTime>> GetEventsForDate(DateTime? datetime = null)
        {

            Expression<Func<EventTime, bool>> exp;
            if (datetime.HasValue)
            {
                exp = x => x.Date.Date == datetime.Value.Date;
            }
            else
            {
                exp = x => x.Date.Date == DateTime.Now.Date;
            }

            List<EventTime> eventTimes = await Context.EventTimes.Where(exp).OrderBy(x => x.Date).ToListAsync();

            return eventTimes;
        }
    }
}

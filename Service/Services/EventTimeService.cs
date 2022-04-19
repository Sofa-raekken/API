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

        public async Task<bool> DeleteEventTimestap(int timestampId)
        {
            EventTime entity = await Context.EventTimes.SingleOrDefaultAsync(x => x.IdEventTime == timestampId);
            if(entity is null)
            {
                return false;
            }
            Context.EventTimes.Remove(entity);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<List<EventTime>> GetAllEventTimestamps()
        {
            return await Context.EventTimes.Include(x => x.FkIdEventNavigation).ToListAsync();
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

            List<EventTime> eventTimes = await Context.EventTimes.Include(x => x.FkIdEventNavigation).Where(exp).OrderBy(x => x.Date).ToListAsync();

            return eventTimes;
        }

        public async Task<bool> InsertEventTimestampsForDate(List<EventTime> eventTimestamps)
        {
            await Context.EventTimes.AddRangeAsync(eventTimestamps);

            return await Context.SaveChangesAsync() > 0;
        }
    }
}

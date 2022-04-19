using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventTimeService
    {
        Task<List<EventTime>> GetEventsForDate(DateTime? datetime = null);
        Task<List<EventTime>> GetAllEventTimestamps();
        Task<bool> InsertEventTimestampsForDate(List<EventTime> eventTimestamps);
        Task<bool> DeleteEventTimestap(int timestampId);

    }
}

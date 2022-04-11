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

    }
}

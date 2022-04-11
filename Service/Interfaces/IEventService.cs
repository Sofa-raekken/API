using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetEvents();
        Task<Event> GetEvent(int id);
        Task<Event> InsertEvent(CreateEventDTO eventDTO);
        Task<bool> DeleteEvent(int id);
        Task<Event> UpdateEvent(int id, UpdateEventDTO eventDTO);
    }
}

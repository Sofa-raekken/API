using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Service.Services
{
    public class EventService : IEventService
    {
        private Kbh_zooContext Context;
        public EventService(Kbh_zooContext context)
        {
            Context = context;
        }
        public async Task<List<Event>> GetEvents()
        {
            return await Context.Events.Include(x => x.AnimalHasEvents).ThenInclude(x => x.AnimalIdAnimalNavigation).ToListAsync();
        }
    }
}

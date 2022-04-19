using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Data.DTO;

namespace Service.Services
{
    public class EventService : IEventService
    {
        private Kbh_zooContext Context;
        public EventService(Kbh_zooContext context)
        {
            Context = context;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            try
            {
                Event eventModel = await Context.Events.SingleOrDefaultAsync(x => x.IdEvent == id);

                if (eventModel is null) { return false; }

                Context.Events.Remove(eventModel);

                List<AnimalHasEvent> animalHasEvents = await Context.AnimalHasEvents.Where(x => x.EventIdEvent == id).ToListAsync();
                Context.AnimalHasEvents.RemoveRange(animalHasEvents);

                return await Context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Event> GetEvent(int id)
        {
            return await Context.Events.Include(x => x.AnimalHasEvents).ThenInclude(x => x.AnimalIdAnimalNavigation).SingleOrDefaultAsync(x => x.IdEvent == id);
        }

        public async Task<List<Event>> GetEvents()
        {
            return await Context.Events.Include(x => x.AnimalHasEvents).ThenInclude(x => x.AnimalIdAnimalNavigation).ToListAsync();
        }

        public async Task<Event> InsertEvent(CreateEventDTO eventDTO)
        {
            try
            {
                Event eventInsert = new Event()
                {
                    Name = eventDTO.Name,
                    Description = eventDTO.Description
                };

                eventInsert.AnimalHasEvents.Add(new AnimalHasEvent() { AnimalIdAnimal = eventDTO.IdAnimal });

                Context.Events.Add(eventInsert);

                if (await Context.SaveChangesAsync() > 0)
                {
                    return eventInsert;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Event> UpdateEvent(int id, UpdateEventDTO eventDTO)
        {
            Event eventModel = await Context.Events.Include(x => x.AnimalHasEvents).ThenInclude(x => x.AnimalIdAnimalNavigation).SingleOrDefaultAsync(x => x.IdEvent == id);

            if(eventModel is null) { return null; }

            eventModel.Name = eventDTO.Name;
            eventModel.Description = eventDTO.Description;

            if (await Context.SaveChangesAsync() > 0)
            {
                return eventModel;
            }
            return null;
        }
    }
}

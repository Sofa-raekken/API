using Data.DTO;
using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AnimalService : IAnimalService
    {
        public AnimalService()
        {
            
        }

        public Task<bool> DeleteAnimal(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Animal> FullUpdateAnimal(UpdateAnimalDTO animal)
        {
            throw new NotImplementedException();
        }

        public async Task<Animal> GetAnimal(int id)
        {
            using (kbh_zooContext cts = new kbh_zooContext())
            {
                Animal animal = await cts.Animals.SingleOrDefault(x => x.IdAnimal == id);
                return animal;
            }
        }

        public Task<List<Animal>> GetAnimals()
        {
            throw new NotImplementedException();
        }

        public Task<Animal> InsertAnimal(CreateAnimalDTO animal)
        {
            throw new NotImplementedException();
        }
    }
}

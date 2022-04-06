using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IAnimalRepository : IGenericRepository
    {
        void InsertAnimal(CreateAnimalDTO animal);
        void DeleteAnimal(int id);
        void UpdateAnimal(UpdateAnimalDTO animal);
        Animal GetAnimal(int id);
        List<Animal> GetAnimals();

        
    }
}

using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAnimalService
    {
        Task<Animal> GetAnimal(int id);
        Task<List<Animal>> GetAnimals();
        Task<Animal> InsertAnimal(CreateAnimalDTO animal);
        Task<bool> DeleteAnimal(int id);
        Task<Animal> FullUpdateAnimal(UpdateAnimalDTO animal);
    }
}

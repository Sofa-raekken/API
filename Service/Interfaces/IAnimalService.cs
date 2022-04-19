﻿using Data.DTO;
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
        Animal GetAnimal(int id);
        Task<List<Animal>> GetAnimals(bool alsoDisabled);
        Task<int> InsertAnimal(Animal animal, List<DietDTO> diets);
        Task<bool> DeleteAnimal(int id);
        Task<Animal> FullUpdateAnimal(int id,UpdateAnimalDTO animal);
        Task<bool> UpdateQRCodeAnimal(int id, string path);
    }
}

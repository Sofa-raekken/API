using Data.DTO;
using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Service.Services
{
    public class AnimalService : IAnimalService
    {
        private Kbh_zooContext Context;
        public AnimalService(Kbh_zooContext ctx)
        {
            Context = ctx;
        }

        public async Task<bool> DeleteAnimal(int id)
        {
            Animal animal = await Context.Animals.SingleOrDefaultAsync(x => x.IdAnimal == id);

            if (animal is null) { return false; }

            animal.Disabled = 1;

            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<Animal> FullUpdateAnimal(int id, UpdateAnimalDTO animal)
        {
            try
            {
                Animal animalEntity = await Context.Animals.SingleOrDefaultAsync(x => x.IdAnimal == id);

                if (animal is null) { return null; }

                MapUpdateAnimalDTOToAnimal(animal, ref animalEntity);

                return await Context.SaveChangesAsync() > 0 ? animalEntity : null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Animal GetAnimal(int id)
        {
            Animal animal = Context.Animals
                .Include(x => x.AnimalHasDiets).ThenInclude(x => x.DietIdDietNavigation)
                .Include(x => x.AnimalHasEvents).ThenInclude(x => x.EventIdEventNavigation)
                .Include(x => x.SpeciesIdSpeciesNavigation)
                .SingleOrDefault(x => x.IdAnimal == id);
            return animal;
        }

        public async Task<List<Animal>> GetAnimals(bool alsoDisabled)
        {
            Expression<Func<Animal, bool>> exp;

            if (alsoDisabled)
            {
                exp = x => x.Disabled == 0 || x.Disabled == 1;
            }
            else
            {
                exp = x => x.Disabled == 0;
            }

            List<Animal> animals = await Context.Animals
                .Include(x => x.AnimalHasDiets).ThenInclude(x => x.DietIdDietNavigation)
                .Include(x => x.AnimalHasEvents).ThenInclude(x => x.EventIdEventNavigation).Where(exp)
                .ToListAsync();
            return animals;
        }

        public async Task<int> InsertAnimal(Animal animal, List<DietDTO> diets)
        {
            try
            {

                if (Context.Animals.Where(x => x.Name == animal.Name || x.LatinName == animal.LatinName).Count() > 0)
                {
                    return 0;
                }

                foreach (var diet in diets)
                {
                    var dietExist = await Context.Diets.SingleOrDefaultAsync(x => x.IdDiet == diet.IdDiet);
                    if (dietExist is not null)
                    {
                        animal.AnimalHasDiets.Add(new AnimalHasDiet()
                        {
                            DietIdDiet = diet.IdDiet
                        });
                    }
                    else
                    {
                        Diet newDiet = new Diet()
                        {
                            Diet1 = diet.Diet1
                        };

                        animal.AnimalHasDiets.Add(new AnimalHasDiet()
                        {
                            DietIdDietNavigation = newDiet
                        });
                    }
                }
                Context.Animals.Add(animal);

                return await Context.SaveChangesAsync() > 0 ? animal.IdAnimal : 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateQRCodeAnimal(int id, string path)
        {
            try
            {
                Animal animalEntity = await Context.Animals.SingleOrDefaultAsync(x => x.IdAnimal == id);

                if (animalEntity is null) { return false; }

                animalEntity.Qr = path;

                return await Context.SaveChangesAsync() > 0 ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void MapUpdateAnimalDTOToAnimal(UpdateAnimalDTO from, ref Animal to)
        {
            to.Name = from.Name;
            to.LatinName = from.LatinName;
            to.Description = from.Description;
            to.Weight = from.Weight;
            to.LifeExpectancy = from.LifeExpectancy;
            to.Pregnancy = from.Pregnancy;
            to.Heigth = from.Heigth;
            to.BirthWeight = from.BirthWeight;
            to.Qr = from.Qr;
            to.SpeciesIdSpecies = from.SpeciesIdSpecies;
            to.Disabled = (byte?)(from.Disabled == true ? 1 : 0);
        }
    }
}
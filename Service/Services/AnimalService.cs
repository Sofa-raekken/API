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
        public AnimalService()
        {
        }

        public async Task<bool> DeleteAnimal(int id)
        {
            using (kbh_zooContext ctx = new kbh_zooContext())
            {
                Animal animal = await ctx.Animals.SingleOrDefaultAsync(x => x.IdAnimal == id);

                if(animal is null) { return false; }

                animal.Disabled = 1;

                return ctx.SaveChanges() > 0 ? true : false;
            }
        }

        public async Task<Animal> FullUpdateAnimal(int id,UpdateAnimalDTO animal)
        {
            try
            {
                using (kbh_zooContext ctx = new kbh_zooContext())
                {
                    Animal animalModel = await ctx.Animals.SingleOrDefaultAsync(x => x.IdAnimal == id);

                    if (animal is null) { return null; }

                    MapUpdateAnimalDTOToAnimal(animal, ref animalModel);

                    return ctx.SaveChanges() > 0 ? animalModel : null;
                }
            }
            catch (Exception)
            {

                return null;
            }

        }

        public Animal GetAnimal(int id)
        {
            using (kbh_zooContext ctx = new kbh_zooContext())
            {
                Animal animal = ctx.Animals
                    .Include(x => x.AnimalHasDiets).ThenInclude(x => x.DietIdDietNavigation)
                    .Include(x => x.AnimalHasEvents).ThenInclude(x => x.EventIdEventNavigation)
                    .SingleOrDefault(x => x.IdAnimal == id);
                return animal;
            }
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

            using (kbh_zooContext cts = new kbh_zooContext())
            {
                List<Animal> animals = await cts.Animals
                    .Include(x => x.AnimalHasDiets).ThenInclude(x => x.DietIdDietNavigation)
                    .Include(x => x.AnimalHasEvents).ThenInclude(x => x.EventIdEventNavigation).Where(exp)
                    .ToListAsync();
                return animals;
            }
        }

        public async Task<int> InsertAnimal(Animal animal, List<DietDTO> diets)
        {
            try
            {
                using (kbh_zooContext ctx = new kbh_zooContext())
                {
                    if (ctx.Animals.Where(x => x.Name == animal.Name || x.LatinName == animal.LatinName).Count() > 0)
                    {
                        return 0;
                    }

                    foreach (var diet in diets)
                    {
                        var dietExist = await ctx.Diets.SingleOrDefaultAsync(x => x.IdDiet == diet.IdDiet);
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
                    ctx.Animals.Add(animal);

                    return await ctx.SaveChangesAsync() > 0 ? animal.IdAnimal : 0;
                }
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
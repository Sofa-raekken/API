using Data.DTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private Kbh_zooContext context;

        public AnimalRepository(Kbh_zooContext ctx)
        {
            context = ctx;
        }
        public void DeleteAnimal(int id)
        {
            throw new NotImplementedException();
        }

        public Animal GetAnimal(int id)
        {
            throw new NotImplementedException();
        }

        public List<Animal> GetAnimals()
        {
            throw new NotImplementedException();
        }

        public void InsertAnimal(CreateAnimalDTO animal)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateAnimal(UpdateAnimalDTO animal)
        {
            throw new NotImplementedException();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

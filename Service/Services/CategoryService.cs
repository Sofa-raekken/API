using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private Kbh_zooContext Context { get; }

        public CategoryService(Kbh_zooContext context)
        {
            Context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await Context.Categories.ToListAsync();
        }
    }
}

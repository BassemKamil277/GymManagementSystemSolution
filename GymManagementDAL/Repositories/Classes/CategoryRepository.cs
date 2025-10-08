using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext dbContext;

        public CategoryRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Category category)
        {
            dbContext.Categories.Add(category);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
           var Cat = dbContext.Categories.Find(id);
            if (Cat == null) return 0;
            dbContext.Categories.Remove(Cat);
           return dbContext.SaveChanges();

        }

        public IEnumerable<Category> GetAll() => dbContext.Categories.ToList();
       

        public Category? GetById(int id) => dbContext.Categories.Find(id);
      

        public int Update(Category category)
        {
           dbContext.Categories.Update(category);
            return dbContext.SaveChanges();
        }
    }
}

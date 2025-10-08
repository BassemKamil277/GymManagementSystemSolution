using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext dbContext;

        public PlanRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Plan plan)
        {
            dbContext.Plans.Add(plan);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var plan = dbContext.Plans.Find(id);
            if (plan == null) return 0;
            dbContext.Remove(plan);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Plan> GetAll() => dbContext.Plans.ToList();
      

        public Plan? GetById(int id) => dbContext.Plans.Find(id);
       

        public int Update(Plan plan)
        {
           dbContext.Plans.Update(plan);
            return dbContext.SaveChanges();
        }
    }
}

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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext dbContext;

        public TrainerRepository(GymDbContext dbContext )
        {
            this.dbContext = dbContext;
        }

        public int Add(Trainer trainer)
        {
            dbContext.Trainers.Add( trainer );
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var Trainer = dbContext.Trainers.Find(id);
            if (Trainer == null) return 0;
            dbContext.Trainers.Remove(Trainer);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll() => dbContext.Trainers.ToArray().ToList();
        

        public Trainer? GetById(int id) => dbContext.Trainers.Find(id);
     

        public int Update(Trainer trainer)
        {
            dbContext.Trainers.Update(trainer);
            return dbContext.SaveChanges();
        }
    }
}

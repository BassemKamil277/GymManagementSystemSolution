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
    internal class MemberShipRepository : IMemberShipRepository
    {
        private readonly GymDbContext dbContext;

        public MemberShipRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(MemberShip memberShip)
        {
            dbContext.MemberShips.Add(memberShip);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var MS = dbContext.MemberShips.Find(id);
            if (MS == null) return 0; 
            dbContext.MemberShips.Remove(MS);
            return dbContext.SaveChanges();
        }

        public IEnumerable<MemberShip> GetAll() => dbContext.MemberShips.ToList();


        public MemberShip? GetById(int id) => dbContext.MemberShips.Find(id);
     

        public int Update(MemberShip memberShip)
        {
           dbContext.MemberShips.Update(memberShip);
            return dbContext.SaveChanges();
        }
    }
}

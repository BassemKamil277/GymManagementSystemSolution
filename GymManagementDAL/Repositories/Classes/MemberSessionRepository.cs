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
    internal class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymDbContext dbContext;

        public MemberSessionRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(MemberSession memberSession)
        {
            dbContext.MemberSessions.Add(memberSession);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var MS = dbContext.MemberSessions.Find(id);
            if (MS == null) return 0;
            dbContext.Remove(MS);
            return dbContext.SaveChanges();
        }

        public IEnumerable<MemberSession> GetAll() => dbContext.MemberSessions.ToList();
     

        public MemberSession? GetById(int id) => dbContext.MemberSessions.Find(id);
      

        public int Update(MemberSession memberSession)
        {
            dbContext.MemberSessions.Update(memberSession);
            return dbContext.SaveChanges();
        }
    }
}

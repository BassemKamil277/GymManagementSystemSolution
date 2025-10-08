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
    internal class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly GymDbContext dbContext;

        public HealthRecordRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(HealthRecord record)
        {
            dbContext.HealthRecords.Add(record);
            return dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
           var HR = dbContext.HealthRecords.Find(id);
            if (HR == null) return 0;
            dbContext.HealthRecords.Remove(HR);
            return dbContext.SaveChanges();


        }

        public IEnumerable<HealthRecord> GetAll() => dbContext.HealthRecords.ToList();
     

        public HealthRecord? GetById(int id) => dbContext.HealthRecords.Find(id);
     

        public int Update(HealthRecord record)
        {
            dbContext.HealthRecords.Update(record);
            return dbContext.SaveChanges();
        }
    }
}

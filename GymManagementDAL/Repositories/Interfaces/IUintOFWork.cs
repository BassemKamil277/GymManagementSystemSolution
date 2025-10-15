﻿using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUintOFWork
    {
        public ISessionRepository SessionRepository { get; }
        IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        int SaveChanges();
    }
}

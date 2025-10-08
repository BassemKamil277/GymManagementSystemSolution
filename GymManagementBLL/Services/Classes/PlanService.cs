using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUintOFWork _uintOFWork;

        public PlanService(IUintOFWork uintOFWork)
        {
           _uintOFWork = uintOFWork;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _uintOFWork.GetRepository<Plan>().GetAll();
            if (plans == null || !plans.Any()) return [];

            return plans.Select(x => new PlanViewModel()
            {
                Description = x.Description,
                DurationDays = x.DurationDays,
                Id = x.Id,  
                IsActive = x.IsActive,
                Name = x.Name,
                Price = x.Price,

            });
        }

        public PlanViewModel? GetPlanDetails(int id)
        {
            var plan = _uintOFWork.GetRepository<Plan>().GetById(id);
            if (plan == null) return null;

            return new PlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Id = plan.Id,
                IsActive = plan.IsActive,
                Name = plan.Name,
                Price = plan.Price,
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _uintOFWork.GetRepository<Plan>().GetById(planId);
            if(plan is null || plan.IsActive == false || HasActiveMemberShips(planId)) 
                return null;

            return new UpdatePlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                PlanName = plan.Name,
                Price = plan.Price,
            };
        }

        public bool UpdatePlan(int planId, UpdatePlanViewModel UpdatePlan)
        {
            try
            {
                var plan = _uintOFWork.GetRepository<Plan>().GetById(planId);
                if (plan is null || plan.IsActive == false || HasActiveMemberShips(planId))
                    return false;

                // tari2a gdeda lel update
                (plan.Description, plan.DurationDays, plan.Price, plan.UpdatedAt) =
                (UpdatePlan.Description, UpdatePlan.DurationDays, UpdatePlan.Price, DateTime.Now);

                _uintOFWork.GetRepository<Plan>().Update(plan);
                return _uintOFWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool toggleStatus(int planId)
        {
            var plan = _uintOFWork.GetRepository<Plan>().GetById(planId);
            if (plan is null || plan.IsActive == false || HasActiveMemberShips(planId))
                return false;

            plan.IsActive = plan.IsActive == true ? false : true;
            try
            {
                _uintOFWork.GetRepository<Plan>().Update(plan);
                return _uintOFWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
         

        }


        #region Methods
        private bool HasActiveMemberShips(int planId)
        {
            return _uintOFWork.GetRepository<MemberShip>()
                   .GetAll(x => x.Id == planId).Any();
        }
        #endregion
    }
}

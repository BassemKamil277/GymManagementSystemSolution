using GymManagementBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanDetails(int id);

        UpdatePlanViewModel? GetPlanToUpdate(int planId);

        bool UpdatePlan(int planId, UpdatePlanViewModel UpdatePlan);

        bool toggleStatus(int planId);


    }
}

using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GymManagementPl.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        #region GettAlll
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        #endregion
        
        #region Details
         public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Invalid plan id";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanDetails(id);
            if (plan == null)
            {
                TempData["Error Message"] = "plan not found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Invalid plan id";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["Error Message"] = "plan not found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        [HttpPost]

        public ActionResult Edit([FromRoute]int id , UpdatePlanViewModel updatePlan)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongDate" , "please check validations");
                return View(updatePlan);
            }
            var result = _planService.UpdatePlan(id, updatePlan);
            if (result)
            {
                TempData["Success Message"] = "Plan updated successfuly";
            }
            else
            {
                TempData["Error Message"] = "Plan failed updated";
            }
            return RedirectToAction(nameof(Index));


        }

        public ActionResult Activate(int id)
        {
            var result = _planService.toggleStatus(id);
            if (result)
            {
                TempData["Success Message"] = "Plan status changed";
            }
            else
            {
                TempData["Error Message"] = "failed to change status";

            }
            return RedirectToAction(nameof(Index));

        }
        #endregion


    }
}

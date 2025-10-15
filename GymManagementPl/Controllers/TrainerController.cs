using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        #region GetAll
        public ActionResult Index()
        {
            var Trainer = _trainerService.GetAllTrainers();
            return View(Trainer);
        }

        #endregion

        #region create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerVieModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(nameof(Create) , model);
            }

            var result = _trainerService.CreateTrainer(model);
            if (result)
            {
                TempData["Success Message"] = "Trainer created successfuly";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError ("" , "Trainer faild create");
                return View(model);
            }
        }

        #endregion

        #region getTrainerDetails
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Id of Trainer can not be 0 or Negative";
                return View(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerDetails(id);
            if(Trainer == null)
            {
                TempData["Error Message"] = "Trainer not found";
                return View(nameof(Index));
            }
            return View(Trainer);
        }
        #endregion

        #region Update
        public ActionResult TrainerEdit(int id)
        {
            if(id <=0 )
            {
                TempData["Error Message"] = "Id of Trainer can not be 0 or Negative";
                return View(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerToUpdate(id);
            if(Trainer == null)
            {
                TempData["Error Message"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            return View(Trainer);
        }
        [HttpPost]
        public ActionResult TrainerEdit([FromRoute] int id ,   UpdateTrainerViewModel updateTrainer )
        {
            if (!ModelState.IsValid)
                return View(updateTrainer);

            var result = _trainerService.UpdateTrainer(id, updateTrainer);
            if (result)
            {
                TempData["Success Message"] = "Trainer Update successfuly";
            }
            else
            {
                TempData["Error Message"] = "Trainer faild Update";
            }
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #region delete
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Id of Trainer can not be 0 or Negative";
                return View(nameof(Index));
            }
            var Trainer = _trainerService.GetTrainerToUpdate(id);
            if (Trainer == null)
            {
                TempData["Error Message"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            return View();
        }
        public ActionResult DeleteConfirm(int id)
        {
            var result = _trainerService.DeleteTrainer(id);
            if (result)
            {
                TempData["Success Message"] = "Trainer delete successfuly";
            }
            else
            {
                TempData["Error Message"] = " Trainer faild to delete";

            }
            return RedirectToAction(nameof(Index));
       
        }
        #endregion
    }
}

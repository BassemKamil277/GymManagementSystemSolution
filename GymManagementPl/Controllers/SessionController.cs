using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPl.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region GetAll
        public IActionResult Index()
        {
            var sessions = _sessionService.GelAll();
            return View(sessions);
        }
        #endregion

        #region Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Invalid Session id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["Error Message"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        #endregion

        #region Create
        public ActionResult Create()
        {
            LoadCategoryDropDown();
            LoadTrainerDropDown();

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadCategoryDropDown();
                LoadTrainerDropDown();
                return View(viewModel);
            }
            var result = _sessionService.CreateSession(viewModel);
            if (result)
            {
                TempData["Success Message"] = "Session created successfuly";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error Message"] = "Session failed created";
                LoadCategoryDropDown();
                LoadTrainerDropDown();
                return View(viewModel);

            }

        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Invalid Session id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionToUpdate(id);
            if (session == null)
            {
                TempData["Error Message"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            LoadTrainerDropDown();
            return View(session);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id , UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                LoadTrainerDropDown();
                return View(updateSession);
            }
            var result = _sessionService.UpdateSession( updateSession , id);
            if (result)
            {
                TempData["Success Message"] = "Session updated successfuly";            
            }
            else
            {
                TempData["Error Message"] = "Session failed updated";            
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Invalid Session id";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionById(id);
            if (session == null)
            {
                TempData["Error Message"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _sessionService.RemoveSession(id);
            if (result)
            {
                TempData["Success Message"] = "Session deleted successfuly";
            }
            else
            {
                TempData["Error Message"] = "Session failed deleted";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helper
        private void LoadTrainerDropDown()
        {
            var Trainers = _sessionService.GetAllTrainersForDropDown();
            ViewBag.Trainers = new SelectList(Trainers, "Id", "Name");
        }

        private void LoadCategoryDropDown()
        {
            var Categories = _sessionService.GetAlCategpriesForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        }
        #endregion

    }
}

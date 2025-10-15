using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModel;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPl.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        #region GetAll
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        #endregion

        #region GetMemberData
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);

            if (member == null)
            {
                TempData["Error Message"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }
        #endregion

        #region GetHealthRecordDetails
        public ActionResult HealthRecordDetails(int id)
        {
            if (id == 0)
            {
                TempData["Error Message"] = "Id of HealthRecord can not be 0 or Negative";    
                return RedirectToAction(nameof(Index));
            }

            var HealthRecord = _memberService.GetMemberHealthRecordDetails(id);
            if(HealthRecord is null)
            {
                TempData["Error Message"] = " HealthRecord not found";
                return RedirectToAction(nameof(Index));
            }

            return View(HealthRecord);

        }
        #endregion

        #region AddMember

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost] // da el verb el lazm a7oto 3la ay action by3dl fe data 
        public ActionResult CreateMember(CreateMemberViewModel create)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check your data");
                return View(nameof(Create) , create); // create el tania 34an lma y3ml add w el process mtkmlsh el data el md5lha s7 mt3dhash mn el awl
                
            }
            bool result = _memberService.CreateMember(create);
            if(result)
            {
                TempData["Success Message"] = "Member created successfuly";
            }
            else
            {
                TempData["Error Message"] = "Member faild created , check phone and email";

            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Update
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberToUpdate(id);
            if (Member is null)
            {
                TempData["Error Message"] = " Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View(Member);
        }

        [HttpPost]
        public ActionResult MemberEdit( [FromRoute] int id , MemberToUpdateViewModel ViewModel)
        {
            if(!ModelState.IsValid)
                return View(ViewModel);

            var result = _memberService.UpdateMemberDetails(id , ViewModel);
            if (result)
            {
                TempData["Success Message"] = "Member Update successfuly";
            }
            else
            {
                TempData["Error Message"] = "Member faild Update , check phone and email";

            }
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region remove
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Id of Member can not be 0 or Negative";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberService.GetMemberDetails(id);
            if (Member is null)
            {
                TempData["Error Message"] = " Member not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId= id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirm([FromRoute]int id)
        {
            var result = _memberService.RemoveMember(id);
            if(result)
            {
                TempData["Success Message"] = "Member delete successfuly";
            }
            else
            {
                TempData["Error Message"] = " Member faild to delete";

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}

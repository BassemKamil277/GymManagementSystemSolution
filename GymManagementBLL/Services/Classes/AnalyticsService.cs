using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AnalyticsViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUintOFWork _uintOFWork;

        public AnalyticsService(IUintOFWork uintOFWork)
        {
            _uintOFWork = uintOFWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions = _uintOFWork.SessionRepository.GetAll();
            return new AnalyticsViewModel()
            {
                
                ActiveMembers = _uintOFWork.GetRepository<MemberShip>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = _uintOFWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _uintOFWork.GetRepository<Trainer>().GetAll().Count(),
                UpComingSessions = sessions.Count(x => x.StartDate > DateTime.Now),
                OnGoingSessions = sessions.Count(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now),
                CompletedSessions = sessions.Count(x => x.EndDate < DateTime.Now)
            };
        }
    }
}

using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ISessionService
    {
        IEnumerable<SessionViewModel> GelAll(); 

        SessionViewModel? GetSessionById(int id);

        bool CreateSession(CreateSessionViewModel createSession);

        UpdateSessionViewModel? GetSessionToUpdate(int SessionId);

        bool UpdateSession (UpdateSessionViewModel updateSession , int SessionId);

        
        bool RemoveSession(int  SessionId);

    }
}

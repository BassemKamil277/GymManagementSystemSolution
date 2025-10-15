using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class SessionService : ISessionService
    {
        // 34an a3ml auto mapping lazm atlob mn el CLR y3mly obj mn interface esmha IMapper
        private readonly IUintOFWork _uintOFWork;
        private readonly IMapper _mapper;

        public SessionService(IUintOFWork uintOFWork , IMapper mapper)
        {
            _uintOFWork = uintOFWork;
            _mapper = mapper;
        }


        public IEnumerable<SessionViewModel> GelAll()
        {
            var sessions = _uintOFWork.SessionRepository.GetAllSessionWithTrainersAndGategories();
            if (sessions == null || !sessions.Any()) return [];

            #region manual mapping
            //return sessions.Select(x => new SessionViewModel()
            //{
            //    // lazm yb2a kol haga b nafs el esm  34an yfhm el aouto mapping 
            //    Id = x.Id,
            //    Capacity = x.Capacity,
            //    Description = x.Description,
            //    StartDate = x.StartDate,
            //    EndDate = x.EndDate,
            //    TrainerName = x.SessionTrainer.Name,  // f kda da msh haynf3 
            //    CategoryName = x.SessionCategory.CategoryName,
            //    AvilableSlots = x.Capacity - _uintOFWork.SessionRepository.GetCountOfBookedSlots(x.Id),
            //});

            #endregion

            #region Auto mapping

            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            return MappedSessions;

            #endregion

        }

        public SessionViewModel? GetSessionById(int id)
        {
            var session = _uintOFWork.SessionRepository.GetSessionByIdWithTrainersAndGategories(id);
            if(session == null) return null;

            var MappeedSession = _mapper.Map<Session , SessionViewModel>(session);
            return MappeedSession;

        }
        public bool CreateSession(CreateSessionViewModel createSession)
        {
          try
            {
                if (!IsTrainerExist(createSession.TrainerId)) return false;
                if (!IsCategoryExist(createSession.CategoryId)) return false;
                if (!IsValidDateRange(createSession.StartDate, createSession.EndDate)) return false;

                var MappedSession = _mapper.Map<CreateSessionViewModel, Session>(createSession);
                _uintOFWork.SessionRepository.Add(MappedSession);
                return _uintOFWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var session = _uintOFWork.SessionRepository.GetById(SessionId);
            if (!IsSessionAvilableForUpdating(session!)) return null;
            return _mapper.Map<Session, UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(UpdateSessionViewModel updateSession, int SessionId)
        {
         try
            {
                var session = _uintOFWork.SessionRepository.GetById(SessionId);
                if (!IsSessionAvilableForUpdating(session!)) return false;
                if (!IsTrainerExist(updateSession.TrainerId)) return false;
                if (!IsValidDateRange(updateSession.StartDate, updateSession.EndDate)) return false;

                _mapper.Map(updateSession, session);
                session!.UpdatedAt = DateTime.Now;
               _uintOFWork.SessionRepository.Update(session); // bs hatta lw mktbthash hay3ml update 
                return _uintOFWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveSession(int SessionId)
        {
            var session = _uintOFWork.SessionRepository.GetById(SessionId);
             try
             {

                if (!IsSessionAvilableForRemoving(session!)) return false;
                _uintOFWork.SessionRepository.Delete(session);
                return _uintOFWork.SaveChanges() > 0;
             }
            catch
            {
                return false;
            }
        }

        #region Helper Method
        private bool IsTrainerExist(int TrainerId)
        {
            return _uintOFWork.GetRepository<Trainer>().GetById(TrainerId) is not null;
        }
        private bool IsCategoryExist(int CategoryId)
        {
            return _uintOFWork.GetRepository<Category>().GetById(CategoryId) is not null;
        }
        private bool IsValidDateRange(DateTime StartDate , DateTime EndDate)
        {
            return StartDate < EndDate && StartDate > DateTime.Now;
        }
        private bool IsSessionAvilableForUpdating(Session session)
        {
            if(session != null) return false;

            if(session.EndDate < DateTime.Now) return false;

            if(session.StartDate <= DateTime.Now) return false;

            var HasActiveBooking = _uintOFWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }

        private bool IsSessionAvilableForRemoving(Session session)
        {
            if (session != null) return false;

            if (session.StartDate > DateTime.Now) return false;

            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            var HasActiveBooking = _uintOFWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }


        #endregion

    }
}

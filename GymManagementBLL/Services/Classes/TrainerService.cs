using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModel;
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
    internal class TrainerService : ITrainerService
    {
        private readonly IUintOFWork _uintOFWork;
        private readonly IMapper _mapper;

        public TrainerService(IUintOFWork uintOFWork , IMapper mapper)
        {
            _uintOFWork = uintOFWork;
            _mapper = mapper;
        }
        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
             var trainer = _uintOFWork.GetRepository<Trainer>().GetAll();
            if (trainer == null || !trainer.Any()) return [];

            //return trainer.Select(x => new TrainerViewModel()
            //{
            //    Name = x.Name,
            //    Email = x.Email,
            //    Phone = x.Phone,
            //    Specialties = x.Specialties.ToString()
            //});

            var TrainerMapping = _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerViewModel>>(trainer);
            return TrainerMapping;

        }

        public TrainerViewModel? GetTrainerDetails(int id)
        {
            var trainer = _uintOFWork.GetRepository<Trainer>().GetById(id);
            if (trainer == null) return null;

            //return new TrainerViewModel()
            //{
            //    Name = trainer.Name,
            //    Email = trainer.Email,
            //    Phone = trainer.Phone,
            //    Specialties = trainer.Specialties.ToString()
            //};

            var TrainerMapping = _mapper.Map<Trainer, TrainerViewModel>(trainer);
            return TrainerMapping;
        }

        public bool CreateTrainer(CreateTrainerVieModel model)
        {
            if (EmailExists(model.Email) || PhoneExists(model.Phone)) return false;

            //var trainer = new Trainer()
            //{
            //    Name = model.Name,
            //    Email = model.Email,
            //    Phone = model.Phone,
            //    Gender = model.Gender,
            //    DateOfBirth = model.DateOfBirth,
            //    Specialties = model.Specialties
            //};
            var trainer = _mapper.Map<CreateTrainerVieModel, Trainer>(model); 
            _uintOFWork.GetRepository<Trainer>().Add(trainer);
            return _uintOFWork.SaveChanges() > 0;
        }

        public UpdateTrainerViewModel? GetTrainerToUpdate(int TrainerId)
        {
           var trainer = _uintOFWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer == null) return null;

            //return new UpdateTrainerViewModel()
            //{

            //    Email = trainer.Email,
            //    Phone = trainer.Phone,
            //    BuildingNumber = trainer.Address.BuildingNumber,
            //    Street = trainer.Address.Street,
            //    City = trainer.Address.City,
            //    Specialties = trainer.Specialties
            //};

            return _mapper.Map<Trainer, UpdateTrainerViewModel>(trainer);

        }

        public bool UpdateTrainer(int TrainerId, UpdateTrainerViewModel UpdateTrainer)
        {
           try
            {
                var trainer = _uintOFWork.GetRepository<Trainer>().GetById(TrainerId);
                if (trainer == null) return false;

                //trainer.Email = UpdateTrainer.Email;
                //trainer.Phone = UpdateTrainer.Phone;
                //trainer.Address.BuildingNumber = UpdateTrainer.BuildingNumber;
                //trainer.Address.Street = UpdateTrainer.Street;
                //trainer.Address.City = UpdateTrainer.City;
                //trainer.Specialties = UpdateTrainer.Specialties;
                trainer.CreatedAt = DateTime.Now;

                _mapper.Map<UpdateTrainerViewModel, Trainer>(UpdateTrainer);
                _uintOFWork.GetRepository<Trainer>().Update(trainer);
                return _uintOFWork.SaveChanges() > 0;

            }
            catch
            {
                return false;
            }
           
        }

        public bool DeleteTrainer(int TrainerId)
        {
            var trainer = _uintOFWork.GetRepository<Trainer>().GetById(TrainerId);
            if (EmailExists(trainer.Email) || PhoneExists(trainer.Phone)) return false;

            var HaveSessions = _uintOFWork.GetRepository<Session>()
                               .GetAll(x => x.Id == TrainerId && x.StartDate > DateTime.Now).Any();

            if(HaveSessions) return false; 

            _uintOFWork.GetRepository<Trainer>().Delete(trainer);
            return _uintOFWork.SaveChanges() > 0;
     
        }

        #region Methods 
       private bool EmailExists(string Email)
        {
            return _uintOFWork.GetRepository<Trainer>().GetAll(x => x.Email == Email).Any();
        }

        private bool PhoneExists(string Phone)
        {
            return _uintOFWork.GetRepository<Trainer>().GetAll(x => x.Phone == Phone).Any();
        }
        #endregion

    }
}

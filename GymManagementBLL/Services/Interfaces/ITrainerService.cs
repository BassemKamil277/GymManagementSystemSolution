using GymManagementBLL.ViewModels.TrainerViewModel;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();

        bool CreateTrainer(CreateTrainerVieModel model);

        TrainerViewModel? GetTrainerDetails(int id);

        UpdateTrainerViewModel? GetTrainerToUpdate(int TrainerId);

        bool UpdateTrainer(int TrainerId , UpdateTrainerViewModel UpdateTrainer);

        bool DeleteTrainer(int TrainerId);

    }
}

using AutoMapper;
using GymManagementBLL.ViewModels.MemberViewModel;
using GymManagementBLL.ViewModels.PlanViewModel;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementBLL.ViewModels.TrainerViewModel;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL
{
    public class MappingProfile : Profile // bwrs mno 34an hwa gwah haga esmha createMap 
    {
        public MappingProfile()
        {
            #region Session Mapping
            CreateMap<Session, SessionViewModel>()
                .ForMember(destnation => destnation.TrainerName,
                options => options.MapFrom(source => source.SessionTrainer.Name))

                .ForMember(dest => dest.CategoryName,
                option => option.MapFrom(src => src.SessionCategory.CategoryName))

                .ForMember(dest => dest.AvilableSlots, options => options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap(); // reverse =  CreateMap<UpdateSessionViewModel , Session>() 
            #endregion

            #region Member Mapping
            CreateMap<Member, MemberViewModel>();

            CreateMap<CreateMemberViewModel, Member>();
                   
            CreateMap<HealthRecord, HealthRecordViewModel>();

            CreateMap<Member, MemberToUpdateViewModel>().ReverseMap();
            #endregion

            #region Plan Mapping
            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, UpdatePlanViewModel>().ReverseMap();
            #endregion

            #region Trainer Mapping
            CreateMap<Trainer, TrainerViewModel>();

            CreateMap<CreateTrainerVieModel, Trainer>();

            CreateMap<Trainer, UpdateTrainerViewModel>()
                     .ForMember(dest => dest.BuildingNumber, option =>
                                option.MapFrom(src => src.Address.BuildingNumber))

                     .ForMember(dest => dest.Street, option =>
                                option.MapFrom(src => src.Address.Street))

                     .ForMember(dest => dest.City, option =>
                                option.MapFrom(src => src.Address.City)).ReverseMap(); 
            #endregion


        }
    }
}

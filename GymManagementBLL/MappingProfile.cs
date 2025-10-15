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
            MapSession();

            MapMember();

            MapPlan(); 

            MapTrainer();


        }

        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
               .ForMember(destnation => destnation.TrainerName,
               options => options.MapFrom(source => source.SessionTrainer.Name))

               .ForMember(dest => dest.CategoryName,
               option => option.MapFrom(src => src.SessionCategory.CategoryName))

               .ForMember(dest => dest.AvilableSlots, options => options.Ignore());

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap(); // reverse =  CreateMap<UpdateSessionViewModel , Session>()

            CreateMap<Trainer, TrainerSelectViewModel>();

            CreateMap<Category, CategorySelectViewModel>()
                      .ForMember(dest => dest.Name,
                      option => option.MapFrom(src => src.CategoryName));
        }

        private void MapMember()
        {
            CreateMap<Member, MemberViewModel>()
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City,
                }))

                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => new HealthRecord()
                {
                    Height = src.HealthRecordViewModel.Height,
                    Weight = src.HealthRecordViewModel.weight,
                    BloodType = src.HealthRecordViewModel.BloodType,
                    Note = src.HealthRecordViewModel.Note,

                }));


                     

            CreateMap<HealthRecord, HealthRecordViewModel>();

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore()) // b3ml ignore 34an dol mynf3sh a3mlhom edit fl update
                .ForMember(dest => dest.Phone, opt => opt.Ignore())
                .AfterMap((src , dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street; 
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                    

                });


        }

        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, UpdatePlanViewModel>()
                    .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdatePlanViewModel, Plan>()
                 .ForMember(dest => dest.Name, opt => opt.Ignore())
                 .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        }

        private void MapTrainer()
        {
            CreateMap<Trainer, TrainerViewModel>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));


            CreateMap<CreateTrainerVieModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City,
                }));

            CreateMap<Trainer, UpdateTrainerViewModel>()
                     .ForMember(dest => dest.BuildingNumber, option =>
                                option.MapFrom(src => src.Address.BuildingNumber))

                     .ForMember(dest => dest.Street, option =>
                                option.MapFrom(src => src.Address.Street))

                     .ForMember(dest => dest.City, option =>
                                option.MapFrom(src => src.Address.City));

            CreateMap<UpdateTrainerViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });

   
        }
    }
}

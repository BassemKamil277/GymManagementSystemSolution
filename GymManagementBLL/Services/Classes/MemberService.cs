using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUintOFWork _uintOFWork;
        private readonly IMapper _mapper;

        public MemberService(IUintOFWork uintOFWork , IMapper mapper)
        {
            _uintOFWork = uintOFWork;
            _mapper = mapper;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Member = _uintOFWork.GetRepository<Member>().GetAll();
            if (Member == null || !Member.Any()) return []; // [] == Enumrable.Empty<MemberViewModel>();

            // ana 3aiz arg3 haga mn no3 MemberViewModel
            // Member - MemberViewModel => Mapping 

            #region manual mapping
            //var memberViewModel = Member.Select(x => new MemberViewModel
            //{
            //    Name = x.Name,
            //    Id = x.Id,
            //    Email = x.Email,
            //    Phone = x.Phone,
            //    Photo = x.Photo,
            //    Gender = x.Gender.ToString(),

            //});
            //    return memberViewModel; 
            #endregion

            var member = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(Member);
            return member;
        }

        public bool CreateMember(CreateMemberViewModel viewModel)
        {
          try
            {
                //check email is exsist
                //check phone is exsist
                if (IsEmailExists(viewModel.Email) || IsPhoneExists(viewModel.Phone)) return false;


                #region manual mapping
                //// mapping
                //var Member = new Member()
                //{
                //    Name = viewModel.Name,
                //    Email = viewModel.Email,
                //    Phone = viewModel.Phone,
                //    Gender = viewModel.Gender,
                //    DateOfBirth = viewModel.DateOfBirth,
                //    Address = new Address()
                //    {
                //        BuildingNumber = viewModel.BuildingNumber,
                //        City = viewModel.City,
                //        Street = viewModel.Street,
                //    },

                //    HealthRecord = new HealthRecord()
                //    {
                //        Height = viewModel.HealthRecordViewModel.Height,
                //        Weight = viewModel.HealthRecordViewModel.weight,
                //        BloodType = viewModel.HealthRecordViewModel.BloodType,
                //        Note = viewModel.HealthRecordViewModel.Note,

                //    }

                //}; 
                //_uintOFWork.GetRepository<Member>().Add(Member);
                //return _uintOFWork.SaveChanges() > 0;
                #endregion

                var member = _mapper.Map<CreateMemberViewModel, Member>(viewModel);
                _uintOFWork.GetRepository<Member>().Add(member);
                return _uintOFWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        public MemberViewModel? GetMemberDetails(int MemberId)
        {
           var member = _uintOFWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;

            var mappedMember = _mapper.Map<Member, MemberViewModel>(member);
            #region manual 
            // manual mappng from memberViewModel to Member
            //var ViewModel = new MemberViewModel()
            //{
            //    Name = member.Name,
            //    Email = member.Email,
            //    Phone = member.Phone,
            //    Photo = member.Photo,
            //    Gender = member.Gender.ToString(),
            //    DateOfBirth = member.DateOfBirth.ToShortDateString(),
            //    Address = $"{member.Address.BuildingNumber} -{member.Address.Street} - {member.Address.City}"

            //}; 
            #endregion



            var ActiveMemberShip = _uintOFWork.GetRepository<MemberShip>()
                             .GetAll(x => x.MemberId == MemberId && x.Status == "Active")
                             .FirstOrDefault();

            if (ActiveMemberShip is not null)
            {
                mappedMember.MemberShipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                mappedMember.MemberShipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
                var Plan = _uintOFWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                mappedMember.PlanName = Plan?.Name;
            }
            return mappedMember;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = _uintOFWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (MemberHealthRecord == null) return null;

            // HealthRecord - HealthRecordViewMode
            //return new HealthRecordViewModel()
            //{
            //    Height = MemberHealthRecord.Height,
            //    weight = MemberHealthRecord.Weight,
            //    BloodType = MemberHealthRecord.BloodType,
            //    Note = MemberHealthRecord.Note
            //};
            return _mapper.Map<HealthRecord , HealthRecordViewModel>(MemberHealthRecord); 
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member = _uintOFWork.GetRepository<Member>().GetById(MemberId);
            if(member == null) return null;

            return _mapper.Map<Member , MemberToUpdateViewModel>(member);
            //return new MemberToUpdateViewModel()
            //{
            //    Photo = member.Photo,
            //    Name = member.Name,
            //    Phone = member.Phone,
            //    BuildingNumber = member.Address.BuildingNumber,
            //    Street = member.Address.Street,
            //    City = member.Address.City,
            //};

        } // da 34an y3rd el details abl el update

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel memberToUpdate)
        {
           try
            {
                if (IsEmailExists(memberToUpdate.Email) || IsPhoneExists(memberToUpdate.Phone)) return false;

                var member = _uintOFWork.GetRepository<Member>().GetById(Id);
                if (member == null) return false;

                //member.Email = memberToUpdate.Email; // el member dy el data ely ma3roda ely 3aiz a8yrha 
                //member.Phone = memberToUpdate.Phone;
                //member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                //member.Address.Street = memberToUpdate.Street;
                //member.Address.City = memberToUpdate.City;
                member.CreatedAt = DateTime.Now; // 34an ana m5leha t update fy akher 2t et3ml feh changes

                _mapper.Map(memberToUpdate, member);
                _uintOFWork.GetRepository<Member>().Update(member) ; 
                return _uintOFWork.SaveChanges() > 0;
            } 
            catch
            {
                return false;
            }
        } // y3rd el data b3d el update 

        public bool RemoveMember(int MemberId)
        {
            try
            {
                var member = _uintOFWork.GetRepository<Member>().GetById(MemberId);
                if(member == null) return false;

                var HasActiveMemberSession = _uintOFWork.GetRepository<MemberSession>()
                 .GetAll(x => x.Id == MemberId && x.Session.StartDate > DateTime.Now).Any();

                if (HasActiveMemberSession) return false;

                var MemberShip = _uintOFWork.GetRepository<MemberShip>().GetAll(x => x.MemberId ==  MemberId);
                if (MemberShip.Any())
                {
                    foreach( var membership in MemberShip)
                        _uintOFWork.GetRepository<MemberShip>().Delete(membership);
                }

                 _uintOFWork.GetRepository<Member>().Delete(member);
                return _uintOFWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }



        #region methods - method etkrrt aktr mn mara ( createMember - UpdateMember)
        private bool IsEmailExists(string email)
        {
           return _uintOFWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _uintOFWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }

     

        #endregion
    }
}

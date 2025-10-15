using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AccountViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser? ValidateUser(AccountViewModel accountViewModel)
        {
           var User = _userManager.FindByEmailAsync(accountViewModel.Email).Result;

            if (User is null)
            {
                return null;
            }

            var IsPAsswordValid = _userManager.CheckPasswordAsync(User, accountViewModel.Password).Result;
            return IsPAsswordValid ? User : null;   
        }
    }
}

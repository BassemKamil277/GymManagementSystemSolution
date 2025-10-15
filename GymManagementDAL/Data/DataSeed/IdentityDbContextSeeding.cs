using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {

        public static bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            try
            {
               var HasUsers = userManager.Users.Any();
               var HasRoles = roleManager.Roles.Any();

                if (HasRoles && HasUsers) return false;

                if(!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new () {Name = "SuperAdmin"},
                        new () {Name = "Admin"}
                    };
                    foreach(var Role in Roles) // da bdl el addRange l2n el IdentityRole mfhash addRange
                    {
                        if(!roleManager.RoleExistsAsync(Role.Name!).Result) // b3ml check bs hya mawgoda wlla la2 ziadt ta2ked
                        {
                            roleManager.CreateAsync(Role).Wait();
                        }
                    }
                }

                if(!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Shady",
                        LastName = "Kamil",
                        UserName = "ShadyKamil",
                        Email = "ShadyKamil@gmail.com",
                        PhoneNumber = "1234567890",
                    };
                    userManager.CreateAsync (MainAdmin ,"P@assw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Mostafa",
                        LastName = "Hany",
                        UserName = "MostafaHany",
                        Email = "MostafaHany@gmail.com",
                        PhoneNumber = "1234568971",
                    };
                    userManager.CreateAsync(Admin, "P@assw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();

                }

                return true;
             
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed faild : {ex}");
                return false;
            }
        }
    }
}

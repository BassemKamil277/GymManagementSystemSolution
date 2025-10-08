using GymManagementDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    // da class 3mlto 34an el Member and trainer entities moshtrken fe kol el properties 
    // w khaleto ywrs mn el baseEntity 34an class el membar yb2a feh kol el properties ely m7tagha w yb2a wares mn el baseentity b tari2a 8er mobashra 

    public class GymUser : BaseEntity 
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Address Address { get; set; } = null!;

    }

    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }

        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}

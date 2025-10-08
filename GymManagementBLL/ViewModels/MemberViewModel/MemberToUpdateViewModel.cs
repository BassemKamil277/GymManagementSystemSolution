using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.MemberViewModel
{
    internal class MemberToUpdateViewModel
    {
        public string Name { get; set; } = null!; // msh ha7ot 3leh validation 34an hwa hayb2a ma3rod bs msh haynf3 a8yro 
        public string Photo { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be Between 5 and 100")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; } = null!;



        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid phone format")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(010||011||012)\d{8}$", ErrorMessage = "phone number must be egyption number")]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage = "BuildingNumber Is Required")]
        [Range(1, 1000, ErrorMessage = "BuildingNumber must be Between 0 and 1000")]
        public int BuildingNumber { get; set; }


        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be Between 2 and 30")]
        public string Street { get; set; } = null!;


        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be Between 2 and 30")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must contain only this letters")]
        public string City { get; set; } = null!;

    }
}

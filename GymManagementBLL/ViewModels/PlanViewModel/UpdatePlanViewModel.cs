using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.ViewModels.PlanViewModel
{
    internal class UpdatePlanViewModel
    {

        public string PlanName { get; set; } = null!;


        [Required(ErrorMessage = "Description Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be Between 2 and 50")]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = "DurationDays Is Required")]
        [Range(1, 365, ErrorMessage = "DurationDays must be Between 1 and 365")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price Is Required")]
        [Range(0.1, 1000, ErrorMessage = "Price must be Between 0.1 and 1000")]
        public decimal Price { get; set; }
         
    }
}

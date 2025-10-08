using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Entities
{
    public class MemberShip : BaseEntity
    {
        // StartDate == CreatedAt in BaseEntity
        public DateTime EndDate { get; set; }

        // Read only property - y3ny msh hayb2a coloumn fy el DB da bs hay3ml check 
        public string Status 
        {
            get 
            {
                if (EndDate == DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            
            }
        }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }

        public Plan Plan { get; set; } = null!;
    }
}

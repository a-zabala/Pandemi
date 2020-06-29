using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.Models
{
    public class Food
    {
        public int ID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
       // public string Recipe { get; set; }

        public FamilyMember FamilyMember { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}

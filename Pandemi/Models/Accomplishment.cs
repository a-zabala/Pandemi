using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.Models
{
    public class Accomplishment
    {
        public int ID { get; set; }
        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        [Required]
        [Display(Name="Skill/Accomplishment")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public FamilyMember FamilyMember { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.Models
{
    public class Book
    {
        public string Title { get; set; }
        public int ID { get; set; }
        
        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        public string Notes { get; set; }
        public string Author { get; set; }

        public FamilyMember FamilyMember { get; set; }
       
    }
}

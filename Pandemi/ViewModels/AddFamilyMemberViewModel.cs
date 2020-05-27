using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class AddFamilyMemberViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter some text for your entry")]
        public int Age { get; set; }
        public int ID { get; set; }
        public DateTime EntryDate { get; set; }

        //public List<SelectListItem> FamilyMembers { get; set; }


        public AddFamilyMemberViewModel() {}

    }
}

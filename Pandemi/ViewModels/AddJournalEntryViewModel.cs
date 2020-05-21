using Pandemi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class AddJournalEntryViewModel
    {
        [Required]
        [Display(Name = "Author Name")]
        public string FamilyMember { get; set; }

        [Required(ErrorMessage = "You must enter some text for your entry")]
        public string Entry { get; set; }

        [Required]
        [Display(Name = "FamilyMember")]
        public int FamilyMemberID { get; set; }

        public List<SelectListItem> FamilyMembers { get; set; }

        public AddJournalEntryViewModel() { }
        public AddJournalEntryViewModel(IEnumerable<FamilyMember> familymembers)
        {

            FamilyMembers = new List<SelectListItem>();
            foreach (FamilyMember familymember in familymembers)
            {

                FamilyMembers.Add(new SelectListItem
                {
                    Value = familymember.ID.ToString(),
                    Text = familymember.FirstName
                });
            }
        }
    }
}

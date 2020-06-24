using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class EditJournalEntryViewModel
    {
        public string FamilyMember { get; set; }
        //public int ID { get; set; }

        [Required(ErrorMessage = "You must enter some text for your entry")]
        public string Entry { get; set; }

        [Required]
        [Display(Name = "Author Name")]
        public int FamilyMemberID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EntryDate { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Entry File")]
        public string EntryFile { get; set; }
        //public IFormFile EntryFile { get; set; }
        public string FileName { get; set; }
        public int ID { get; set; }


        public List<SelectListItem> FamilyMembers { get; set; }

        public EditJournalEntryViewModel()
        {

        }
        public EditJournalEntryViewModel(IEnumerable<FamilyMember> familymembers)
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

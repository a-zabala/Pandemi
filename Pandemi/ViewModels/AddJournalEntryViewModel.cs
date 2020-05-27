﻿using Pandemi.Models;
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
        public string FamilyMember { get; set; }

        [Required(ErrorMessage = "You must enter some text for your entry")]
        public string Entry { get; set; }

        [Required]
        [Display(Name = "Author Name")]
        public int FamilyMemberID { get; set; }
        public DateTime EntryDate { get; set; }


        public List<SelectListItem> FamilyMembers { get; set; }

        public AddJournalEntryViewModel() 
        {
            EntryDate = DateTime.Now;
        }
        public AddJournalEntryViewModel(IEnumerable<FamilyMember> familymembers)
        {
            EntryDate = DateTime.Now;
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

﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class AddBookViewModel
    {
        [Required]
        public string Title{ get; set; }

        
        public string Notes { get; set; }
        public int ID { get; set; }
        public string Author { get; set; }
        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        public List<SelectListItem> FamilyMembers { get; set; }

        public AddBookViewModel(){}
        public AddBookViewModel(IEnumerable<FamilyMember> familymembers) 
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

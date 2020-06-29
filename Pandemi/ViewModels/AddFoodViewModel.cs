using Microsoft.AspNetCore.Mvc.Rendering;
using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class AddFoodViewModel
    {
        [Required]
        public string Name { get; set; }


        public string Notes { get; set; }
        public int ID { get; set; }
        public string Website { get; set; }
        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        public List<SelectListItem> FamilyMembers { get; set; }
        public string UserId { get; set; }

        public AddFoodViewModel() { }
        public AddFoodViewModel(IEnumerable<FamilyMember> familymembers)
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

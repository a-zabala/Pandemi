using Microsoft.AspNetCore.Mvc.Rendering;
using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class EditAccomplishmentViewModel
    {

        [Display(Name = "Family Member")]
        public int FamilyMemberID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }


        public List<SelectListItem> FamilyMembers { get; set; }

        public EditAccomplishmentViewModel()
        {

        }
        public EditAccomplishmentViewModel(IEnumerable<FamilyMember> familymembers)
        {
            Date = DateTime.Now;
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

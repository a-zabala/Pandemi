using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pandemi.Models
{
    public class AppUser: IdentityUser
    {
        [PersonalData,Required,StringLength(20)]
        public string FamilyName { get; set; }

    }
}

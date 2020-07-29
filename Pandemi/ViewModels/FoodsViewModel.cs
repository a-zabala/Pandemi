using Pandemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandemi.ViewModels
{
    public class FoodsViewModel
    {
        
        public List<FamilyMember> FamilyMembers { get; set; }
       
        public List<Food> Foods { get; set; }
        
        public FamilyMember FamilyMember { get; set; }
    }
}

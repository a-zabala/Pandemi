using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandemi.Data;
using Pandemi.Models;
using Pandemi.ViewModels;

namespace Pandemi.Controllers
{
    public class FamilyMemberController : Controller
    {
        private readonly ApplicationDbContext context;

        public FamilyMemberController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            
            List<FamilyMember> familymembers = context.FamilyMembers.ToList();
            return View(familymembers);
            
        }
        public IActionResult Add()
        {
            AddFamilyMemberViewModel addFamilyMemberViewModel = 
                new AddFamilyMemberViewModel();
            return View(addFamilyMemberViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddFamilyMemberViewModel addFamilyMemberViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing cheeses
                FamilyMember newFamilyMember = new FamilyMember
                {
                    FirstName = addFamilyMemberViewModel.FirstName,
                    Age = addFamilyMemberViewModel.Age
                };

                context.FamilyMembers.Add(newFamilyMember);
                context.SaveChanges();

                return Redirect("/FamilyMember");
            }

            return View(addFamilyMemberViewModel);
        }
    }
}
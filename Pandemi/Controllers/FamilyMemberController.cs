using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandemi.Data;
using Pandemi.Models;
using Pandemi.ViewModels;
using Microsoft.Extensions.Identity.Core;

namespace Pandemi.Controllers
{
    public class FamilyMemberController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> _userManager;

        public FamilyMemberController(ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            IList<FamilyMember> familymembers = context.FamilyMembers.Where(s => s.UserId == user.Id).ToList();
            return View(familymembers);
            
        }
        public IActionResult Add()
        {
            AddFamilyMemberViewModel addFamilyMemberViewModel = 
                new AddFamilyMemberViewModel();
            return View(addFamilyMemberViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddFamilyMemberViewModel addFamilyMemberViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                // Add the new familymember to my existing familymember list
                FamilyMember newFamilyMember = new FamilyMember
                {
                    FirstName = addFamilyMemberViewModel.FirstName,
                    Age = addFamilyMemberViewModel.Age,
                    UserId = user.Id
                };

                context.FamilyMembers.Add(newFamilyMember);
                context.SaveChanges();

                return Redirect("/FamilyMember");
            }

            return View(addFamilyMemberViewModel);
        }
        //GET FamilyMember/Report/familymember
        public async Task<IActionResult> Report(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var vm = new ReportViewModel();
            vm.Books = context.Books.Include(b => b.FamilyMember).Where(s => s.FamilyMember.ID == id).ToList();
            vm.FamilyMember = context.FamilyMembers.First(s => s.ID == id);
            vm.Foods = context.Foods.Include(b => b.FamilyMember).Where(s => s.FamilyMember.ID == id).ToList();
            vm.Accomplishments = context.Accomplishments.Include(b => b.FamilyMember).Where(s => s.FamilyMember.ID == id).ToList();
            vm.JournalEntries= context.JournalEntries.Include(b => b.FamilyMember).Where(s => s.FamilyMember.ID == id).ToList();

            return View(vm);
        }
    }
}
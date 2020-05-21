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
    public class JournalEntryController : Controller
    {
        private readonly ApplicationDbContext context;

        public JournalEntryController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            IList<JournalEntry> journalentries = context.JournalEntries.Include(c => c.FamilyMember).ToList();

            return View(journalentries);
            //return View();
        }
        public IActionResult Add()
        {
            AddJournalEntryViewModel addJournalEntryViewModel = new AddJournalEntryViewModel(context.FamilyMembers.ToList());
            return View(addJournalEntryViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddJournalEntryViewModel addJournalEntryViewModel)
        {
            if (ModelState.IsValid)
            {
                //FamilyMember newFamilyMember =
                  //  context.FamilyMembers.Single(c => c.ID == addJournalEntryViewModel.FamilyMemberID);
                // Add the new cheese to my existing cheeses
                JournalEntry newJournalEntry = new JournalEntry
                {
                    Name = addJournalEntryViewModel.FamilyMember,
                    Entry = addJournalEntryViewModel.Entry,
                    //FamilyMember = newFamilyMember
                };

                context.JournalEntries.Add(newJournalEntry);
                context.SaveChanges();

                return Redirect("/JournalEntry");
            }

            return View(addJournalEntryViewModel);
        }

    }
}
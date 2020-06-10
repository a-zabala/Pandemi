using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandemi.Data;
using Pandemi.Models;
using Pandemi.ViewModels;
using Humanizer;

namespace Pandemi.Controllers
{
    public class JournalEntryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public JournalEntryController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            context = dbContext;
            webHostEnvironment = hostEnvironment;

        }
        public IActionResult Index()
        //public IActionResult Index()
        {
            IList<JournalEntry> journalentries = context.JournalEntries.Include(c => c.FamilyMember).ToList();

            return View(journalentries);
            //return View();
        }
        public IActionResult Add()
        {
            AddJournalEntryViewModel addJournalEntryViewModel = 
                new AddJournalEntryViewModel(context.FamilyMembers.ToList());
            return View(addJournalEntryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(AddJournalEntryViewModel addJournalEntryViewModel)

        public IActionResult Add(AddJournalEntryViewModel addJournalEntryViewModel)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = UploadedFile(addJournalEntryViewModel);
                FamilyMember newFamilyMember =
                  context.FamilyMembers.Single(c => c.ID == addJournalEntryViewModel.FamilyMemberID);
                // Add the new journal entry to my existing journal entry
                JournalEntry newJournalEntry = new JournalEntry
                {
                    Name = addJournalEntryViewModel.FamilyMember,
                    Entry = addJournalEntryViewModel.Entry,
                    FamilyMember = newFamilyMember,
                    EntryDate = addJournalEntryViewModel.EntryDate,
                    EntryFile = UploadedFile(addJournalEntryViewModel)
                    //EntryPicture = uniqueFileName,
                };

                context.JournalEntries.Add(newJournalEntry);
                //await context.SaveChangesAsync();
                context.SaveChanges();
                //return RedirectToAction(nameof(Index));

                return Redirect("/JournalEntry");
            }

            return View(addJournalEntryViewModel);
        }
        private string UploadedFile(AddJournalEntryViewModel addJournalEntryViewModel)
        {
            string uniqueFileName = null;

            if (addJournalEntryViewModel.EntryFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + addJournalEntryViewModel.EntryFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addJournalEntryViewModel.EntryFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
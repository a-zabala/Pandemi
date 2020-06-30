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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Pandemi.Data.Migrations;

namespace Pandemi.Controllers
{
    [Authorize]
    public class JournalEntryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;

        public JournalEntryController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, UserManager<AppUser> userManager)
        {
            context = dbContext;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
           var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var vm = new JournalEntriesViewModel();

           vm.JournalEntries = context.JournalEntries.Include(c => c.FamilyMember).Where(s => s.UserId == user.Id).ToList();
            vm.FamilyMembers = context.FamilyMembers.Where(s => s.UserId == user.Id).ToList();

           return View(vm);
        }
        public async Task<IActionResult> Add()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            AddJournalEntryViewModel addJournalEntryViewModel = 
                new AddJournalEntryViewModel(context.FamilyMembers.Where(s => s.UserId == user.Id).ToList());
            return View(addJournalEntryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddJournalEntryViewModel addJournalEntryViewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                //string uniqueFileName = UploadedFile(addJournalEntryViewModel);
                FamilyMember newFamilyMember =
                  context.FamilyMembers.Where(s => s.UserId == user.Id).Single(c => c.ID == addJournalEntryViewModel.FamilyMemberID);
                // Add the new journal entry to my existing journal entry
                JournalEntry newJournalEntry = new JournalEntry
                {
                    Name = addJournalEntryViewModel.FamilyMember,
                    Entry = addJournalEntryViewModel.Entry,
                    FamilyMember = newFamilyMember,
                    EntryDate = addJournalEntryViewModel.EntryDate,
                 
                    EntryFile = UploadedFile(addJournalEntryViewModel),
                    UserId = user.Id
                };

                context.JournalEntries.Add(newJournalEntry);
                //await context.SaveChangesAsync();
                context.SaveChanges();
                //return RedirectToAction(nameof(Index));

                return Redirect("/JournalEntry");
            }

            return View(addJournalEntryViewModel);
        }
        // GET: JournalEntry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalentry = await context.JournalEntries
                .Include(b => b.FamilyMember)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (journalentry == null)
            {
                return NotFound();
            }

            return View(journalentry);
        }
        // GET: JournalEntry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null)
            {
                return NotFound();
            }


            var journalEntry = context.JournalEntries.Where(s => s.UserId == user.Id).Include(e => e.FamilyMember).FirstOrDefault(m => m.ID == id);
            AddJournalEntryViewModel addJournalEntryViewModel = new AddJournalEntryViewModel()
            {
               

               Entry = journalEntry.Entry,
                FamilyMemberID = journalEntry.FamilyMemberID,
                EntryDate = journalEntry.EntryDate,
                
              FileName = journalEntry.EntryFile,
             //Name = journalEntry.EntryFile,
             // UserId = journalEntry.User.Id

            };

            if (journalEntry == null)
            {
                return NotFound();
            }
            
            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "FirstName");
            //journalEntry.EntryFile = UploadedFile(addJournalEntryViewModel);
            journalEntry.EntryFile = addJournalEntryViewModel.FileName;

            return View(addJournalEntryViewModel);

        }
        // POST: JournalEntry/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JournalEntry journalentry)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            journalentry.UserId = user.Id;
           //journalentry.EntryFile = journalentry.FileName;


            if (id != journalentry.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    context.Update(journalentry);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalEntryExists(journalentry.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "ID", journalentry.FamilyMemberID);
            //ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers, "ID", "ID", journalentry.FamilyMemberID);
            return View(journalentry);
        }

        // GET: JournalEntry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journalEntry = await context.JournalEntries
                .Include(b => b.FamilyMember)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (journalEntry == null)
            {
                return NotFound();
            }

            return View(journalEntry);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journalEntry = await context.JournalEntries.FindAsync(id);
            context.JournalEntries.Remove(journalEntry);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //GET Book/IndividualEntry/familymember
        public async Task<IActionResult> Individual(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            var vm = new JournalEntriesViewModel();
            vm.JournalEntries = context.JournalEntries.Include(b => b.FamilyMember).Where(s => s.FamilyMember.ID == id).ToList();
            vm.FamilyMember = context.FamilyMembers.First(s => s.ID == id);



            return View(vm);
        }

        private bool JournalEntryExists(int id)
        {
            return context.JournalEntries.Any(e => e.ID == id);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandemi.Data;
using Pandemi.Models;
using Pandemi.ViewModels;

namespace Pandemi.Controllers
{
    [Authorize]
    public class AccomplishmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;

        public AccomplishmentsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, UserManager<AppUser> userManager)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: Accomplishments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var accomplishments = _context.Accomplishments.Include(a => a.FamilyMember).Where(s=>s.UserId == user.Id).ToList();
            return View(accomplishments);
        }

        // GET: Accomplishments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomplishment = await _context.Accomplishments
                .Include(a => a.FamilyMember)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accomplishment == null)
            {
                return NotFound();
            }

            return View(accomplishment);
        }

        // GET: Accomplishments/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            AddAccomplishmentViewModel addAccomplishmentViewModel =
                new AddAccomplishmentViewModel(_context.FamilyMembers.Where(s => s.UserId == user.Id).ToList());
            return View(addAccomplishmentViewModel);
           
        }

        // POST: Accomplishments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FamilyMemberID,Name,Date,Notes,UserId")] AddAccomplishmentViewModel addAccomplishmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                FamilyMember newFamilyMember =
                 _context.FamilyMembers.Where(s => s.UserId == user.Id).Single(c => c.ID == addAccomplishmentViewModel.FamilyMemberID);
                // Add the new accomplishment to my existing accomplishments
                Accomplishment newAccomplishment = new Accomplishment
                {
                    Name = addAccomplishmentViewModel.Name,
                    Notes = addAccomplishmentViewModel.Notes,
                    FamilyMember = newFamilyMember,
                    Date = addAccomplishmentViewModel.Date,
                    UserId = user.Id
                };

                _context.Accomplishments.Add(newAccomplishment);
                _context.SaveChanges();
                return Redirect("/Accomplishments");
            }
            
            return View(addAccomplishmentViewModel);
        }

        // GET: Accomplishments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null)
            {
                return NotFound();
            }

            var accomplishment = _context.Accomplishments.Where(s=> s.UserId == user.Id).Include(e=>e.FamilyMember).FirstOrDefault(m=>m.ID==id);

            EditAccomplishmentViewModel editAccomplishmentViewModel = new EditAccomplishmentViewModel()
            {
                Date = accomplishment.Date,
                Name = accomplishment.Name,
                Notes = accomplishment.Notes,
                FamilyMemberID = accomplishment.FamilyMemberID,
                UserId = accomplishment.User.Id
            };

            if (accomplishment == null)
            {
                return NotFound();
            }
            ViewData["FamilyMemberID"] = new SelectList(_context.FamilyMembers.Where(s=> s.UserId == user.Id), "ID", "FirstName");
            return View(editAccomplishmentViewModel);
        }

        // POST: Accomplishments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FamilyMemberID,Name,Date,Notes,UserId")] Accomplishment accomplishment)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            accomplishment.UserId = user.Id;


            if (id != accomplishment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accomplishment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccomplishmentExists(accomplishment.ID))
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
            ViewData["FamilyMemberID"] = new SelectList(_context.FamilyMembers.Where(s=>s.UserId== user.Id), "ID", "ID", accomplishment.FamilyMemberID);
            return View(accomplishment);
        }

        // GET: Accomplishments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accomplishment = await _context.Accomplishments
                .Include(a => a.FamilyMember)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accomplishment == null)
            {
                return NotFound();
            }

            return View(accomplishment);
        }

        // POST: Accomplishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accomplishment = await _context.Accomplishments.FindAsync(id);
            _context.Accomplishments.Remove(accomplishment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccomplishmentExists(int id)
        {
            return _context.Accomplishments.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;

        public BooksController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, UserManager<AppUser> userManager)
        {
           context = dbContext;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            //IList<FamilyMember> familymembers = context.FamilyMembers.Where(s => s.UserId == user.Id).ToList();
            //return View(familymembers);

            //var userName = User.Identity.Name;
            var books = context.Books.Include(b => b.FamilyMember).Where(s => s.UserId == user.Id).ToList();
            //return View(await applicationDbContext.ToListAsync());
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await context.Books
                .Include(b => b.FamilyMember)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            AddBookViewModel addBookViewModel =
                new AddBookViewModel(context.FamilyMembers.Where(s => s.UserId == user.Id).ToList());
            return View(addBookViewModel);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBookViewModel addBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);


                FamilyMember newFamilyMember =
                  context.FamilyMembers.Where(s => s.UserId == user.Id).Single(c => c.ID == addBookViewModel.FamilyMemberID);
                // Add the new book to my existing books
                Book newBook = new Book
                {
                    Title = addBookViewModel.Title,
                    Notes = addBookViewModel.Notes,
                    FamilyMember = newFamilyMember,
                    Author = addBookViewModel.Author,
                    UserId = user.Id

                };

                context.Books.Add(newBook);
                //await context.SaveChangesAsync();
                context.SaveChanges();
                //return RedirectToAction(nameof(Index));

                return Redirect("/Books"); 
            }
            return View(addBookViewModel);
        }

        // GET: Books/Edit/5
            public async Task<IActionResult> Edit(int? id) { 
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null)
                {
                    return NotFound();
                }

            //Book book= context.Books
            //.Include(e => e.FamilyMember).FirstOrDefaultAsync(m => m.ID == id);
            //var book = context.Books.FindAsync(id);
            
            var book = context.Books.Where(s => s.UserId == user.Id).Include(e=>e.FamilyMember).FirstOrDefault(m=>m.ID==id);
            
            EditBookViewModel editBookViewModel = new EditBookViewModel()
            {
                Author = book.Author,
                Notes = book.Notes,
                Title = book.Title,
                FamilyMemberID = book.FamilyMemberID,
               UserId = book.User.Id

            };




                if (book == null)
                {
                    return NotFound();
                }
           // ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers, "ID", "ID");
           
            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "FirstName");
            //return View(book);
            return View(editBookViewModel);
           
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ID,FamilyMemberID,Notes,Author,UserId")] Book book)

        {
           var user = await _userManager.FindByNameAsync(User.Identity.Name);
            book.UserId = user.Id;

            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(book);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "ID", book.FamilyMemberID);
            return View(book);
        }

        // GET: Books/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await context.Books
                .Include(b => b.FamilyMember)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await context.Books.FindAsync(id);
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return context.Books.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandemi.Data;
using Pandemi.Models;
using Pandemi.ViewModels;

namespace Pandemi.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
           context = dbContext;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Books.Include(b => b.FamilyMember);
            return View(await applicationDbContext.ToListAsync());
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
        public IActionResult Create()
        {
            
            AddBookViewModel addBookViewModel =
                new AddBookViewModel(context.FamilyMembers.ToList());
            return View(addBookViewModel);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult Create(AddBookViewModel addBookViewModel)
        {
            if (ModelState.IsValid)
            {
                FamilyMember newFamilyMember =
                  context.FamilyMembers.Single(c => c.ID == addBookViewModel.FamilyMemberID);
                // Add the new book to my existing books
                Book newBook = new Book
                {
                    Title = addBookViewModel.Title,
                    Notes = addBookViewModel.Notes,
                    FamilyMember = newFamilyMember,
                    Author = addBookViewModel.Author,

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
            public IActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                //ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers, "ID", "ID", book.FamilyMemberID);
                return View(book);
            }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ID,FamilyMemberID,Notes,Author")] Book book)
        {
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
            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers, "ID", "ID", book.FamilyMemberID);
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

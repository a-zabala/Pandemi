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
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;

        public FoodsController(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, UserManager<AppUser> userManager)
        {
            context = dbContext;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var vm = new FoodsViewModel();

            vm.Foods = context.Foods.Include(b => b.FamilyMember).Where(s => s.UserId == user.Id).ToList();
            vm.FamilyMembers = context.FamilyMembers.Where(s => s.UserId == user.Id).ToList();
            return View(vm);

        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await context.Foods
                .Include(b => b.FamilyMember)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Add
        public async Task<IActionResult> Add()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            AddFoodViewModel addFoodViewModel =
                new AddFoodViewModel(context.FamilyMembers.Where(s => s.UserId == user.Id).ToList());
            return View(addFoodViewModel);
        }

        // POST: Foods/Add
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddFoodViewModel addFoodViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);


                FamilyMember newFamilyMember =
                  context.FamilyMembers.Where(s => s.UserId == user.Id).Single(c => c.ID == addFoodViewModel.FamilyMemberID);
                // Add the new book to my existing books
                Food newFood = new Food
                {
                    Name = addFoodViewModel.Name,
                    Notes = addFoodViewModel.Notes,
                    FamilyMember = newFamilyMember,
                    Website = addFoodViewModel.Website,
                    UserId = user.Id

                };

                context.Foods.Add(newFood);
                //await context.SaveChangesAsync();
                context.SaveChanges();
                //return RedirectToAction(nameof(Index));

                return Redirect("/Foods");
            }
            return View(addFoodViewModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (id == null)
            {
                return NotFound();
            }

            //Book book= context.Books
            //.Include(e => e.FamilyMember).FirstOrDefaultAsync(m => m.ID == id);
            //var book = context.Books.FindAsync(id);

            var food = context.Foods.Where(s => s.UserId == user.Id).Include(e => e.FamilyMember).FirstOrDefault(m => m.ID == id);

            AddFoodViewModel addFoodViewModel = new AddFoodViewModel()
            {
                Name = food.Name,
                Notes = food.Notes,
                FamilyMemberID = food.FamilyMemberID,
                Website = food.Website,
                UserId = food.User.Id
            };


            if (food == null)
            {
                return NotFound();
            }
            // ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers, "ID", "ID");

            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "FirstName");
            //return View(book);
            return View(addFoodViewModel);

        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ID,FamilyMemberID,Notes,Website,UserId")] Food food)

        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            food.UserId = user.Id;

            if (id != food.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(food);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.ID))
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
            ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "ID", food.FamilyMemberID);
            return View(food);
        }

        // GET: Books/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await context.Foods
                .Include(b => b.FamilyMember)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await context.Foods.FindAsync(id);
            context.Foods.Remove(food);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //GET Book/IndividualFoods/familymember
        public async Task<IActionResult> Individual(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            var foods = context.Foods.Include(b => b.FamilyMember).Where(s => s.FamilyMember.ID == id).ToList();

            //ViewData["FamilyMemberID"] = new SelectList(context.FamilyMembers.Where(s => s.UserId == user.Id), "ID", "FirstName");
            //ViewData["FamilyMemberName"] = user.;


            return View(foods);
        }

        private bool FoodExists(int id)
        {
            return context.Foods.Any(e => e.ID == id);
        }
    }
}


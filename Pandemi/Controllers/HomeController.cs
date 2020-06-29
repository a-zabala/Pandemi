using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pandemi.Data;
using Pandemi.Models;

namespace Pandemi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> _userManager;



        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _logger = logger;
            context = dbContext;
            _userManager = userManager;

        }

        //public HomeController()
        //{
        //  context = dbContext;
        //}

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            IList<FamilyMember> familymembers = context.FamilyMembers.Where(s => s.UserId == user.Id).ToList();
            return View(familymembers);

            //List<FamilyMember> familymembers = context.FamilyMembers.ToList();
            //return View(familymembers);
        }
        // GET: Home/IndividualInfo/5
        public async Task<IActionResult> IndividualInfo(int? id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id == null)
            {
                return NotFound();
            }

            var familymember = await context.FamilyMembers.FirstOrDefaultAsync(m => m.ID == id);
            if (familymember == null)
            {
                return NotFound();
            }

            return View(familymember);
        }

            
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

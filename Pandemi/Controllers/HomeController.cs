using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            context = dbContext;
        }

        //public HomeController()
        //{
        //  context = dbContext;
        //}

        public IActionResult Index()
        {
            List<FamilyMember> familymembers = context.FamilyMembers.ToList();
            return View(familymembers);
            //return View();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pandemi.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Pandemi.Controllers
{
    public class ImgController : Controller
    {
        private readonly IWebHostEnvironment _iweb;
        public ImgController(IWebHostEnvironment iweb) 
        {
            _iweb = iweb;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile imgfile)
        {
            string ext = Path.GetExtension(imgfile.FileName);
            if((ext==".jpg") || (ext==".gif"))
            {

                var imgsave = Path.Combine(_iweb.WebRootPath, "images", imgfile.FileName);
                var stream = new FileStream(imgsave, FileMode.Create);
                await imgfile.CopyToAsync(stream);
                stream.Close();
            }
            return RedirectToAction("Index");
        }
    }
}

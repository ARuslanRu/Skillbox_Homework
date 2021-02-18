using Homework_20.Data;
using Homework_20.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(
            ILogger<HomeController> logger, 
            AppDbContext db,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            Profile profile = _db.Profile.FirstOrDefault();

            if(profile == null)
            {
                profile = new Profile()
                {
                    LastName = "Фамилия",
                    FirstName = "Имя",
                    MiddleName = "Отчество",
                    About = "Текст о себе",
                    Image = "base.gif"
                };

                _db.Profile.Add(profile);
                _db.SaveChanges();
            }

            return View(profile);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            Profile profile = _db.Profile.FirstOrDefault();

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Profile model)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (files.Count > 0)
                {
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //Если изображение уже было то старое удаляем
                    string oldImage = _db.Profile.AsNoTracking().FirstOrDefault(x=> x.Id == model.Id).Image;
                    if(!string.IsNullOrEmpty(oldImage))
                    {
                        var oldFile = Path.Combine(upload, oldImage);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    model.Image = fileName + extension;

                    _db.Profile.Update(model);
                }
                _db.Profile.Update(model);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
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

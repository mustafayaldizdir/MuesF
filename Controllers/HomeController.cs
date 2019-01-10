using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppDatabasesGuide.Models;
using MuesF.BLL;

namespace AppDatabasesGuide.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Databases()
        {
            List<AppDatabase> databases = MuesBLL<AppDatabase>.MuesOrderByDescending(x => x.CreatedDate).MuesGetAll<AppDatabase>();
            ViewData["Message"] = "Your application description page.";

            return View(databases);
        }

        public IActionResult AddDatabase()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public IActionResult AddDatabase(AppDatabase appDatabase)
        {
            bool result = MuesBLL<AppDatabase>.MuesAdd(appDatabase) > 0;
            if (result)
            {
                ViewData["Message"] = "Başarılı";
            }
            else
            {

                ViewData["Message"] = "Hata";
            }


            return RedirectToAction("Databases");
        }

        public IActionResult UpdateDatabase(int id)
        {
            AppDatabase database = MuesBLL<AppDatabase>.GetTo(id);
            return View(database);
        }

        [HttpPost]
        public IActionResult UpdateDatabase(AppDatabase appDatabase,int id)
        {
            AppDatabase database = MuesBLL<AppDatabase>.GetTo(id);
            database.Name = appDatabase.Name;
            database.Description = appDatabase.Description;

            bool result = MuesBLL<AppDatabase>.MuesAdd(appDatabase) > 0;
            if (result)
            {
                ViewData["Message"] = "Başarılı";
            }
            else
            {

                ViewData["Message"] = "Hata";
            }


            return RedirectToAction("Databases");
        }

        public IActionResult DeleteDatabase(int id)
        {
            
            bool result = MuesBLL<AppDatabase>.RemoveSoft(id) > 0;
            if (result)
            {
                ViewData["Message"] = "Başarılı";
            }
            else
            {

                ViewData["Message"] = "Hata";
            }
            ViewData["Message"] = "Your contact page.";

            return RedirectToAction("Databases");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

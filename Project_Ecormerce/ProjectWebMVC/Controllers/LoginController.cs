using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ProjectWebMVC.Models;

namespace ProjectWebMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: PhoneController

        private readonly Prj301ProjectContext context = new Prj301ProjectContext();
        public ActionResult Index()
        {
            ViewBag.Phones = context.Phones.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
    
            User u = context.Users.Where(c => c.Username == username).FirstOrDefault();
            if (u?.Password == password)
            {
                string userJson = JsonSerializer.Serialize(u);
                HttpContext.Session.SetString("user", userJson);
                return RedirectToAction("Index", "Phone");
            }
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Phone");
        }
        // GET: PhoneController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.phone = context.Phones.Where(p => p.Pid== id).FirstOrDefault();
            ViewBag.phoneDetails = context.PhoneDetails.Where(p => p.Pdid== id).FirstOrDefault();
            return View();
        }

        // GET: PhoneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PhoneController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PhoneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PhoneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhoneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

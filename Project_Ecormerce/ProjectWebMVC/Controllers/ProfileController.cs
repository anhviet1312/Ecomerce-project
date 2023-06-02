using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectWebMVC.Models;

namespace ProjectWebMVC.Controllers
{
    public class ProfileController : Controller
    {
        // GET: ProfileController
        private readonly Prj301ProjectContext context = new Prj301ProjectContext();
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProfileController/Create
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

        // GET: ProfileController/Edit/5
        public ActionResult Edit(string username)
        {
            try { var user = context.Users.Where(u => u.Username == username).FirstOrDefault(); return View(user); }
            catch(Exception ex) {
                return View();
            }

            
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                var existingUser = context.Users.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    existingUser.Address = user.Address;
                    existingUser.Telephone = user.Telephone;
                    existingUser.Password = user.Password;
                  //  existingUser.Username = user.Username;


                    context.Users.Update(existingUser);
                    context.SaveChanges();
                }

                return RedirectToAction("Edit", new { username = existingUser.Username });
            }
            catch
            {
                return View();
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfileController/Delete/5
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

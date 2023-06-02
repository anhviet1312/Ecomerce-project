using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWebMVC.Models;

namespace ProjectWebMVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: ProfileController
        private readonly Prj301ProjectContext context = new Prj301ProjectContext();
        public ActionResult Index(string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                var orders = context.Orders.ToList();
                return View(orders);
            }
            else
            {
                var orders = context.Orders.Where(o => o.User.Username == username).ToList();
                return View(orders);
            }
        }
        // GET: ProfileController/Details/5
        public ActionResult Detail(int id)
        {
            var order = context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.PidNavigation) // Include the Phone navigation property
                .FirstOrDefault();

            return View(order);
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
            var user = context.Users.Where(u => u.Username == username).FirstOrDefault();

            return View(user);
        }

        // POST: ProfileController/Edit/5
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

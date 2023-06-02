using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;

namespace ProjectWebMVC.Controllers
{
    public class CartController : Controller
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
        public ActionResult Checkout()
        {
            try
            {
                var x = HttpContext.Session.GetString("user");
                if (x == null) return RedirectToAction("Index", "Login");
                var u = JsonSerializer.Deserialize<User>(x);
                var cartPhones = JsonSerializer.Deserialize<List<Phone>>(HttpContext.Session.GetString("cartList"));
                var total = 0.0;
                cartPhones.ForEach(p => total += p.Quantity * p.Price);

                if (total <= u.Balance)
                {
                    Order o = new Order
                    {
                        Userid = u.Username,
                        Total = total,
                        Date = DateTime.Now
                    };
                    context.Orders.Add(o);
                    context.SaveChanges();
                    var orderSaved = context.Orders.OrderByDescending(o => o.Id).FirstOrDefault();
                    cartPhones.ForEach(p => {
                        var od = new OrderDetail
                        {
                            Oid = orderSaved.Id,
                            Pid = p.Pid,
                            Quantity = p.Quantity
                        };
                        context.OrderDetails.Add(od);
                        var phone = context.Phones.Where(ph => ph.Pid == p.Pid).FirstOrDefault();
                        phone.Quantity -= p.Quantity;
                        context.Phones.Update(phone);
                        u.Balance -= total;
                        context.Users.Update(u);
                        context.SaveChanges();


                    });
                    HttpContext.Session.Remove("cartList");
                    return RedirectToAction("Index", "Cart");
                }

                TempData["ErrorCheckout"] = "Your balance is not enough";
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                TempData["ErrorCheckout"] = "Error occurs when checkout: " + ex.Message;
                return RedirectToAction("Index", "Cart");
            }
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

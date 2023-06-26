using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectWebMVC.Models;
using System.Collections.Generic;
using System.Security.Cryptography.Pkcs;
using System.Text.Json;
namespace ProjectWebMVC.Controllers
{
    public class PhoneController : Controller
    {
        // GET: PhoneController

        private readonly Prj301ProjectContext context = new Prj301ProjectContext();
        public ActionResult Index()
        {
            ViewBag.Phones = context.Phones.ToList().Where(p => p.Quantity > 0);
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }

            [HttpPost]
            public ActionResult Search(string searchKey, int? category, int? minPrice, int? maxPrice)
            {
                var phonesQuery = context.Phones.Where(p => p.Quantity > 0);

                // Apply search keyword filter
                if (!string.IsNullOrEmpty(searchKey))
                {
                    phonesQuery = phonesQuery.Where(p => p.Name.Contains(searchKey));
                }

                // Apply category filter
                if (category.HasValue)
                {
                    phonesQuery = phonesQuery.Where(p => p.Cid == category.Value);
                }

                // Apply price range filter
                if (minPrice.HasValue)
                {
                    phonesQuery = phonesQuery.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    phonesQuery = phonesQuery.Where(p => p.Price <= maxPrice.Value);
                }

                ViewBag.Phones = phonesQuery.ToList();
                ViewBag.Categories = context.Categories.ToList();
                return View("Index");
            }
        

        public ActionResult ManagePhone()
        {
            ViewBag.Phones = context.Phones.Include(p => p.CidNavigation).ToList();
            return View();
        }
        // GET: PhoneController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.phone = context.Phones.Where(p => p.Pid== id).FirstOrDefault();
            ViewBag.phoneDetails = context.PhoneDetails.Where(p => p.Pdid== id).FirstOrDefault();
            return View();
        }
        
        public ActionResult Buy(int id)
        {
            
            ViewBag.phone = context.Phones.Where(p => p.Pid == id).FirstOrDefault();
            var cartPhones = new List<Phone>();
            if (HttpContext.Session.GetString("cartList")!=null) 
                cartPhones = JsonSerializer.Deserialize<List<Phone>>(HttpContext.Session.GetString("cartList"));
            var phone = cartPhones.Where(p => p.Pid == id).FirstOrDefault();
            if (phone != null)
                {
                    phone.Quantity++;
                }
            else
                {
                    var p = context.Phones.Where(p => p.Pid == id).FirstOrDefault();
                    cartPhones.Add(new Phone { Pid = p.Pid , Name = p.Name, Price = p.Price , Quantity = 1});
                }
            HttpContext.Session.SetString("cartList", JsonSerializer.Serialize(cartPhones));

      
            
            ViewBag.phoneDetails = context.PhoneDetails.Where(p => p.Pdid == id).FirstOrDefault();
            return RedirectToAction("Index" , "Cart");
        }
       
        // GET: PhoneController/Create

        public ActionResult Create()
        {
            var categories = context.Categories.ToList();
            var categoryList = categories.Select(c => new SelectListItem { Value = c.Cid.ToString(), Text = c.Cname }).ToList();
            ViewBag.Categories = new SelectList(categoryList, "Value", "Text");

            return View();
        }
        // POST: PhoneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Phone phone)
        {
            try
            {
                context.Phones.Add(phone);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Create();
            }
        }

        [HttpPost]

        public ActionResult UpdateQuantity(int id, int quantity)
        {
            try
            {
                var cartPhones = JsonSerializer.Deserialize<List<Phone>>(HttpContext.Session.GetString("cartList"));
                var phone = cartPhones.FirstOrDefault(p => p.Pid == id);

                if (phone != null)
                {
                    phone.Quantity = quantity;
                    if (quantity == 0)
                    {
                        cartPhones.Remove(phone);
                    }
                }

                if (cartPhones.Count == 0)
                {
                    HttpContext.Session.Remove("cartList");
                }
                else
                {
                    HttpContext.Session.SetString("cartList", JsonSerializer.Serialize(cartPhones));
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Error occur when updatea quantity");
            }
        }

        

        

        public ActionResult GetPhoneById(int id)
        {
            var phone = context.Phones.Where(p => p.Pid == id).FirstOrDefault(); // Assuming you already have the GetPhoneById method implemented
            return Json(phone);
        }
        // GET: PhoneController/Edit/5
        public ActionResult Edit(int id)
        {
            var phone = context.Phones.Where(p => p.Pid == id).FirstOrDefault();
            var categories = context.Categories.ToList();
            var categoryList = categories.Select(c => new SelectListItem { Value = c.Cid.ToString(), Text = c.Cname }).ToList();
            ViewBag.Categories = new SelectList(categoryList, "Value", "Text", phone.Cid);
            return View(phone);
        }

        // POST: PhoneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Phone phone)
        {
            if (id != phone.Pid)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(phone);
                    context.SaveChanges();
                    return Edit(id);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            var categories = context.Categories.ToList();
            var categoryList = categories.Select(c => new SelectListItem { Value = c.Cid.ToString(), Text = c.Cname }).ToList();
            ViewBag.Categories = new SelectList(categoryList, "Value", "Text", phone.Cid);
            return Edit(id);
        }

        // GET: PhoneController/Delete/5
        public ActionResult Delete(int id)
        {
            var phone = context.Phones.Where(p => p.Pid == id).FirstOrDefault();

            return View(phone);
        }

        // POST: PhoneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var phone = context.Phones.Where(p => p.Pid == id).FirstOrDefault();
            try
            {
                if (phone != null)
                {
                    context.Phones.Remove(phone);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Delete(id);
                }
            }
            catch
            {
                return Delete(id);
            }
        }
    }
}

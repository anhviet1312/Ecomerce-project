using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return View();
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

        public ActionResult GetPhoneById(int id)
        {
            var phone = context.Phones.Where(p => p.Pid == id).FirstOrDefault(); ; // Assuming you already have the GetPhoneById method implemented
            return Json(phone);
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

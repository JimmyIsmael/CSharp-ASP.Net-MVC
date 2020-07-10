using CigarStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using CigarStoreProject.Utils;

namespace CigarStoreProject.Controllers
{
    public class LoginController : Controller
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var data = UserProcessor.GetUserByEmail(loginModel.EmailAddress);
                string encodedPassword = Base64Encode(loginModel.Password);

                if(encodedPassword == data.Password)
                {
                    if (data.IsActive)
                    {
                        if (data.IsAdmin)
                        {
                            Session["isAdmin"] = true;
                            Session["logged"] = true;
                            Session["userId"] = data.Id;
                            Session["emailAddess"] = data.EmailAddress;
                            Session["userName"] = data.FirstName + " " + data.LastName;
                            return RedirectToAction("AdminIndex", "Home", null);
                        }
                        else
                        {
                            Session["isAdmin"] = false;
                            Session["logged"] = true;
                            Session["userId"] = data.Id;
                            Session["emailAddess"] = data.EmailAddress;
                            Session["userName"] = data.FirstName + " " + data.LastName;
                            return RedirectToAction("Index", "Store", null);
                        }
                    }
                }
                ViewBag.error = true;
                return View();
            }
            ViewBag.error = true;
            return View();
        }

        public ActionResult Logout()
        {
            Session["isAdmin"] = false;
            Session["logged"] = false;
            Session["emailAddess"] = "";
            Session["userName"] = "";
            Session["userId"] = "";
            return RedirectToAction("Login");
        }
    }
}
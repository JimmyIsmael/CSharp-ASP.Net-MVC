using CigarStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Diagnostics;

namespace CigarStoreProject.Controllers
{
    public class SignUpController : Controller
    {
        public bool IsActiveSession()
        {
            try
            {
                if ((bool)Session["logged"])
                {
                    ViewBag.isAdmin = (bool)Session["isAdmin"];
                    ViewBag.emailAddess = (string)Session["emailAddess"];
                    ViewBag.userName = (string)Session["userName"];
                    ViewBag.logged = (bool)Session["logged"];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException e)
            {
                return false;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
              int recordsCreated = UserProcessor.CreateUser(userModel.FirstName, userModel.LastName, userModel.CompanyName, userModel.TaxId,
                    userModel.TobaccoLicense, userModel.EmailAddress, Base64Encode(userModel.Password), userModel.Address1,
                    userModel.Address2, userModel.City, userModel.State, userModel.Zip, false,false);
                return RedirectToAction("Confirmation");
            }
            return View();
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult ViewUsers()
        {
            if (IsActiveSession())
            {
                var data = UserProcessor.LoadUsers();
                List<UserModel> users = new List<UserModel>();
                foreach (var row in data)
                {
                    users.Add(new UserModel
                    {
                        Id = row.Id,
                        FirstName = row.FirstName,
                        LastName = row.LastName,
                        CompanyName = row.CompanyName,
                        TaxId = row.TaxId,
                        TobaccoLicense = row.TobaccoLicense,
                        EmailAddress = row.EmailAddress,
                        Password = row.Password,
                        Address1 = row.Address1,
                        Address2 = row.Address2,
                        City = row.City,
                        State = row.State,
                        Zip = row.Zip,
                        IsActive = row.IsActive
                    });
                }
                return View(users);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult UserDetails(int userId)
        {
            if (IsActiveSession())
            {
                var data = UserProcessor.GetUser(userId);
                UserModel model = new UserModel
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    CompanyName = data.CompanyName,
                    TaxId = data.TaxId,
                    TobaccoLicense = data.TobaccoLicense,
                    EmailAddress = data.EmailAddress,
                    Password = data.Password,
                    Address1 = data.Address1,
                    Address2 = data.Address2,
                    City = data.City,
                    State = data.State,
                    Zip = data.Zip,
                    IsActive = data.IsActive
                };
                return View(model);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult EditUser(int userId)
        {
            if (IsActiveSession())
            {
                var data = UserProcessor.GetUser(userId);
                UserModel model = new UserModel
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    CompanyName = data.CompanyName,
                    TaxId = data.TaxId,
                    TobaccoLicense = data.TobaccoLicense,
                    EmailAddress = data.EmailAddress,
                    ConfirmEmail = data.EmailAddress,
                    Password = data.Password,
                    ConfirmPassword = data.Password,
                    Address1 = data.Address1,
                    Address2 = data.Address2,
                    City = data.City,
                    State = data.State,
                    Zip = data.Zip,
                    IsActive = data.IsActive
                };
                return View(model);
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = UserProcessor.UpdateUser(userModel.Id, userModel.FirstName, userModel.LastName, userModel.CompanyName, userModel.TaxId,
                    userModel.TobaccoLicense, userModel.EmailAddress, Base64Encode(userModel.Password), userModel.Address1,
                    userModel.Address2, userModel.City, userModel.State, userModel.Zip, userModel.IsActive);
                return RedirectToAction("ViewUsers");
            }
            return View();
        }
    }
}
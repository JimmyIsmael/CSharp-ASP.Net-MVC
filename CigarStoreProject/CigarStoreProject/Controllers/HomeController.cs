using CigarStoreProject.Models;
using DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CigarStoreProject.Controllers
{
    public class HomeController : Controller
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
            }catch(NullReferenceException e)
            {
                return false;
            }
        }

        public List<CartProductsModel> GetCartProducts(int userId)
        {
            List<CartProductsModel> cartProducts = new List<CartProductsModel>();
            var cartData = CartProcessor.LoadCartProducts(userId);
            float total = 0.0f;
            if (cartData != null)
            {
                foreach (var row in cartData)
                {
                    cartProducts.Add(new CartProductsModel
                    {
                        Id = row.Id,
                        ProductId = row.ProductId,
                        ProductDescription = ProductProcessor.GetProduct(row.ProductId).Description,
                        ProductImagePath = ProductProcessor.GetProduct(row.ProductId).ImagePath,
                        ProductQty = row.ProductQty,
                        ProductPrice = row.ProductPrice,
                        AddedDate = row.AddedDate
                    });
                    total = total + (row.ProductQty * row.ProductPrice);
                }
            }
            ViewBag.Total = total;
            return cartProducts;
        }

        public ActionResult Index()
        {
            if (IsActiveSession())
            {
                return View();
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult About()
        {
            if (IsActiveSession())
            {
                //getting cart products
                int userId = (int)Session["userId"];
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = null,
                    CartProducts = cartProducts
                };
                return View(storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult Contact()
        {
            if (IsActiveSession())
            {
                //getting cart products
                int userId = (int)Session["userId"];
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = null,
                    CartProducts = cartProducts
                };
                return View(storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult AdminIndex()
        {
            if (IsActiveSession())
            {
                return View();
            }
            return RedirectToAction("Login", "Login", null);
        }
    }
}
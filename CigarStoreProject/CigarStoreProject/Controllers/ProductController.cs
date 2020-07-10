using CigarStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.IO;

namespace CigarStoreProject.Controllers
{
    public class ProductController : Controller
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

        // GET: Product
        public ActionResult AddProduct()
        {
            if (IsActiveSession())
            {
                return View();
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                //Saving image to folder
                string fileName = Path.GetFileNameWithoutExtension(productModel.ImageFile.FileName);
                string extension = Path.GetExtension(productModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                productModel.ImagePath = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                productModel.ImageFile.SaveAs(fileName);

                //Saving Product to database
                int recordsCreated = ProductProcessor.CreateProduct(productModel.ProductCode, productModel.Description, productModel.UnitOfMeasure,
                                        productModel.Category, productModel.Price, productModel.ImageTitle, productModel.ImagePath, productModel.IsActive);
                return RedirectToAction("ListProducts");
            }
            return View();
        }

        public ActionResult ListProducts()
        {
            if (IsActiveSession())
            {
                var data = ProductProcessor.LoadProductsAdmin();
                List<ProductModel> products = new List<ProductModel>();
                foreach (var row in data)
                {
                    products.Add(new ProductModel
                    {
                        Id = row.Id,
                        ProductCode = row.ProductCode,
                        Description = row.Description,
                        UnitOfMeasure = row.UnitOfMeasure,
                        Category = row.Category,
                        Price = row.Price,
                        ImageTitle = row.ImageTitle,
                        ImagePath = row.ImagePath,
                        IsActive = row.IsActive
                    });
                }
                return View(products);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult ProductDetail(int id)
        {
            if (IsActiveSession())
            {
                var data = ProductProcessor.GetProduct(id);
                ProductModel product = new ProductModel
                {
                    Id = data.Id,
                    ProductCode = data.ProductCode,
                    Description = data.Description,
                    UnitOfMeasure = data.UnitOfMeasure,
                    Category = data.Category,
                    Price = data.Price,
                    ImageTitle = data.ImageTitle,
                    ImagePath = data.ImagePath,
                    IsActive = data.IsActive
                };
                return View(product);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult EditProduct(int id)
        {
            if (IsActiveSession())
            {
                var data = ProductProcessor.GetProduct(id);
                ProductModel product = new ProductModel
                {
                    Id = data.Id,
                    ProductCode = data.ProductCode,
                    Description = data.Description,
                    UnitOfMeasure = data.UnitOfMeasure,
                    Category = data.Category,
                    Price = data.Price,
                    ImageTitle = data.ImageTitle,
                    ImagePath = data.ImagePath,
                    IsActive = data.IsActive
                };
                return View(product);
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                //Saving image to folder
                string fileName = Path.GetFileNameWithoutExtension(productModel.ImageFile.FileName);
                string extension = Path.GetExtension(productModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                productModel.ImagePath = "~/Images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                productModel.ImageFile.SaveAs(fileName);

                //Updating Product in database
                int recordsCreated = ProductProcessor.UpdateProduct(productModel.Id, productModel.ProductCode, productModel.Description, productModel.UnitOfMeasure,
                                        productModel.Category, productModel.Price, productModel.ImageTitle, productModel.ImagePath, productModel.IsActive);
                return RedirectToAction("ListProducts");
            }
            return View();
        }
    }
}
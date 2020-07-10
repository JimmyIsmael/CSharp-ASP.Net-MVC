using CigarStoreProject.Models;
using DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CigarStoreProject.Controllers
{
    public class StoreController : Controller
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
                //getting store products
                var data = ProductProcessor.LoadProducts();
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

                //getting cart products
                int userId = (int)Session["userId"];
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = products,
                    CartProducts = cartProducts
                };
                return View(storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeachByDescription(string searchValue)
        {
            if (IsActiveSession())
            {
                var data = ProductProcessor.LoadProductsByDescription(searchValue);
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

                //getting cart products
                int userId = (int)Session["userId"];
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = products,
                    CartProducts = cartProducts
                };
                return View("Index", storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeachByCategory(bool cigars, bool vapers)
        {
            if (IsActiveSession())
            {
                return View("Index");
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeachByPriceRange(float minPrice, float maxPrice)
        {
            if (IsActiveSession())
            {
                var data = ProductProcessor.LoadProductsByPriceRange(minPrice, maxPrice);
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

                //getting cart products
                int userId = (int)Session["userId"];
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = products,
                    CartProducts = cartProducts
                };
                return View("Index", storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult ShowProduct(int productId)
        {
            if (IsActiveSession())
            {
                var data = ProductProcessor.GetProduct(productId);
                List<ProductModel> products = new List<ProductModel>();
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
                products.Add(product);
                if (TempData["productAdded"] != null)
                {
                    ViewBag.productAdded = (bool)TempData["productAdded"];
                }
                else
                {
                    ViewBag.productAdded = false;
                }

                //getting cart products
                int userId = (int)Session["userId"];
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = products,
                    CartProducts = cartProducts
                };
                return View(storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductToCart(int productId, int productQty)
        {
            int userId = (int)Session["userId"];
            int recordsCreated = CartProcessor.AddProductToCart(userId, productId, productQty, ProductProcessor.GetProduct(productId).Price);
            TempData["productAdded"] = true;
            return RedirectToAction("ShowProduct", new { productId = productId });
        }

        public ActionResult ViewCart()
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

        public ActionResult DeleteProductFromCart(int productId)
        {
            if (IsActiveSession())
            {
                int userId = (int)Session["userId"];
                int recordsCreated = CartProcessor.DeleteProductFromCart(productId, userId);

                //getting cart products
                List<CartProductsModel> cartProducts = GetCartProducts(userId);

                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = null,
                    CartProducts = cartProducts
                };
                return View("ViewCart", storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult Checkout()
        {
            if (IsActiveSession())
            {
                int userId = (int)Session["userId"];
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
                    Address1 = data.Address1,
                    Address2 = data.Address2,
                    City = data.City,
                    State = data.State,
                    Zip = data.Zip
                };
                ViewBag.userId = model.Id;
                ViewBag.FirstName = model.FirstName;
                ViewBag.LastName = model.LastName;
                ViewBag.CompanyName = model.CompanyName;
                ViewBag.TaxId = model.TaxId;
                ViewBag.TobaccoLicense = model.TobaccoLicense;
                ViewBag.EmailAddress = model.EmailAddress;
                ViewBag.Address1 = model.Address1;
                ViewBag.Address2 = model.Address2;
                ViewBag.City = model.City;
                ViewBag.State = model.State;
                ViewBag.Zip = model.Zip;

                //getting cart products
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

        public ActionResult PlaceOrcer()
        {
            if (IsActiveSession())
            {
                int userId = (int)Session["userId"];
                var data = UserProcessor.GetUser(userId);
                UserModel userModel = new UserModel
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    CompanyName = data.CompanyName,
                    TaxId = data.TaxId,
                    TobaccoLicense = data.TobaccoLicense,
                    EmailAddress = data.EmailAddress,
                    Address1 = data.Address1,
                    Address2 = data.Address2,
                    City = data.City,
                    State = data.State,
                    Zip = data.Zip
                };

                int orderNumber = OrderProcessor.CreateNewOrder(userModel.Id, userModel.FirstName + " " + userModel.LastName, userModel.CompanyName,
                                                userModel.Address1, userModel.City, userModel.State, userModel.Zip, DateTime.Now);
                //setting store products
                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = null,
                    CartProducts = new List<CartProductsModel>()
                };
                ViewBag.Total = 0;
                ViewBag.OrderNumber = "ORD000-" + orderNumber;
                ViewBag.OrderId = orderNumber;
                return View("Confirmation", storeViewModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult ShowOrder(int orderId)
        {
            if (IsActiveSession())
            {
                //Getting header data
                var headerData = OrderProcessor.GetOrderById(orderId);
                OrderModel orderHeader = new OrderModel
                {
                    Id = headerData.Id,
                    Name = headerData.Name,
                    Address = headerData.Address,
                    City = headerData.City,
                    State = headerData.State,
                    Zip = headerData.Zip,
                    CompanyName = headerData.CompanyName,
                    DateCreated = headerData.DateCreated
                };

                //Getting details data
                var detailsData = OrderProcessor.GetOrderProducts(orderId);
                List<OrderProductsModel> orderDetails = new List<OrderProductsModel>();
                float total = 0.0f;
                int index = 0;
                foreach (var row in detailsData)
                {
                    index++;
                    orderDetails.Add(new OrderProductsModel
                    {
                        Id = row.Id,
                        ProductCode = index.ToString(),
                        ProductId = row.ProductId,
                        ProductDescription = row.ProductDescription,
                        ProductPrice = row.ProductPrice,
                        ProductQty = row.ProductQty,
                        ProductImagePath = row.ProductImagePath,
                        OrderId = row.OrderId
                    });
                    total = total + (row.ProductQty * row.ProductPrice);
                }

                CompleteOrderModel completeOrderModel = new CompleteOrderModel
                {
                    OrderHeader = orderHeader,
                    OrderDetails = orderDetails
                };

                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = null,
                    CartProducts = new List<CartProductsModel>()
                };

                ViewBag.total = total;
                ViewBag.taxTotal = total * 0.25;
                ViewBag.grandTotal = total - (total * 0.25);
                return View(completeOrderModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult ShowOrderAdmin(int orderId)
        {
            if (IsActiveSession())
            {
                //Getting header data
                var headerData = OrderProcessor.GetOrderById(orderId);
                OrderModel orderHeader = new OrderModel
                {
                    Id = headerData.Id,
                    Name = headerData.Name,
                    Address = headerData.Address,
                    City = headerData.City,
                    State = headerData.State,
                    Zip = headerData.Zip,
                    CompanyName = headerData.CompanyName,
                    DateCreated = headerData.DateCreated
                };

                //Getting details data
                var detailsData = OrderProcessor.GetOrderProducts(orderId);
                List<OrderProductsModel> orderDetails = new List<OrderProductsModel>();
                float total = 0.0f;
                int index = 0;
                foreach (var row in detailsData)
                {
                    index++;
                    orderDetails.Add(new OrderProductsModel
                    {
                        Id = row.Id,
                        ProductCode = index.ToString(),
                        ProductId = row.ProductId,
                        ProductDescription = row.ProductDescription,
                        ProductPrice = row.ProductPrice,
                        ProductQty = row.ProductQty,
                        ProductImagePath = row.ProductImagePath,
                        OrderId = row.OrderId
                    });
                    total = total + (row.ProductQty * row.ProductPrice);
                }

                CompleteOrderModel completeOrderModel = new CompleteOrderModel
                {
                    OrderHeader = orderHeader,
                    OrderDetails = orderDetails
                };

                StoreViewModel storeViewModel = new StoreViewModel
                {
                    StoreProducts = null,
                    CartProducts = new List<CartProductsModel>()
                };

                ViewBag.total = total;
                ViewBag.taxTotal = total * 0.25;
                ViewBag.grandTotal = total - (total * 0.25);
                return View(completeOrderModel);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult ListOrders()
        {
            if (IsActiveSession())
            {
                var data = OrderProcessor.LoadOrders();
                List<OrderModel> orders = new List<OrderModel>();
                foreach (var row in data)
                {
                    orders.Add(new OrderModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Address = row.Address,
                        City = row.City,
                        State = row.State,
                        Zip = row.Zip,
                        CompanyName = row.CompanyName,
                        DateCreated = row.DateCreated,
                        Status = row.Status,
                        TrackingNumber = row.TrackingNumber
                    });
                }

                return View(orders);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult ListOrdersUser()
        {
            if (IsActiveSession())
            {
                int userId = (int)Session["userId"];
                var data = OrderProcessor.LoadOrdersByUser(userId);
                List<OrderModel> orders = new List<OrderModel>();
                foreach (var row in data)
                {
                    orders.Add(new OrderModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Address = row.Address,
                        City = row.City,
                        State = row.State,
                        Zip = row.Zip,
                        CompanyName = row.CompanyName,
                        DateCreated = row.DateCreated,
                        Status=row.Status,
                        TrackingNumber=row.TrackingNumber
                    });
                }
                return View(orders);
            }
            return RedirectToAction("Login", "Login", null);
        }

        public ActionResult MarkAsShipped(int orderId)
        {
            if (IsActiveSession())
            {
                var headerData = OrderProcessor.GetOrderById(orderId);
                OrderModel orderHeader = new OrderModel
                {
                    Id = headerData.Id,
                    Name = headerData.Name,
                    Address = headerData.Address,
                    City = headerData.City,
                    State = headerData.State,
                    Zip = headerData.Zip,
                    CompanyName = headerData.CompanyName,
                    DateCreated = headerData.DateCreated,
                    Status = headerData.Status
                };
                return View(orderHeader);
            }
            return RedirectToAction("Login", "Login", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsShipped(OrderModel order)
        {
            if (IsActiveSession())
            {
                //Updating Tracking Number
                int recordsUpdated = OrderProcessor.SetOrderTrackingNo(order.Id, order.TrackingNumber);

                //Returning Order List
                var data = OrderProcessor.LoadOrders();
                List<OrderModel> orders = new List<OrderModel>();
                foreach (var row in data)
                {
                    orders.Add(new OrderModel
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Address = row.Address,
                        City = row.City,
                        State = row.State,
                        Zip = row.Zip,
                        CompanyName = row.CompanyName,
                        DateCreated = row.DateCreated,
                        Status = row.Status,
                        TrackingNumber = row.TrackingNumber
                    });
                }
                return View("ListOrders", orders);
            }
            return RedirectToAction("Login", "Login", null);
        }
    }
}
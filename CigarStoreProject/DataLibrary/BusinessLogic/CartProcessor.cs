using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class CartProcessor
    {
        public static CartDataModel GetOpenCart(int userId)
        {
            string sql = @" SELECT [Id]
                              ,[UserId]
                              ,[IsOpen]
                              ,[DateCreated]
                          FROM [dbo].[Cart] where IsOpen=1 and UserId=" + userId;

            return SQLDataAccess.GetRecord<CartDataModel>(sql).FirstOrDefault();
        }

        public static int AddProductToCart(int userId, int productId, int productQty, float productPrice)
        {
            CartDataModel model = GetOpenCart(userId);
            if (model != null) //Adding products to open cart
            {  
                CartProductsDataModel data = new CartProductsDataModel
                {
                    CartId = model.Id,
                    ProductId = productId,
                    ProductQty=productQty,
                    ProductPrice= productPrice,
                    AddedDate = DateTime.Now
                };
                string sql = @"INSERT INTO [dbo].[CartProducts]
                                   ([CartId]
                                   ,[ProductId]
                                   ,[ProductQty]
                                   ,[ProductPrice]
                                   ,[AddedDate])
                             VALUES
                                   (@CartId
                                   ,@ProductId
                                   ,@ProductQty
                                   ,@ProductPrice
                                   ,@AddedDate)";
                return SQLDataAccess.SaveData(sql, data);
            }
            else //Saving New Cart
            {
                CartDataModel cartData = new CartDataModel
                {
                    UserId = userId,
                    IsOpen = true,
                    DateCreated = DateTime.Now
                };
                string sql = @"INSERT INTO [dbo].[Cart]
                                   ([UserId]
                                   ,[IsOpen]
                                   ,[DateCreated])
                             VALUES
                                   (@UserId
                                   ,@IsOpen
                                   ,@DateCreated)";
                SQLDataAccess.SaveData(sql, cartData);

                CartDataModel newCart = GetOpenCart(userId);

                CartProductsDataModel data = new CartProductsDataModel
                {
                    CartId = newCart.Id,
                    ProductId = productId,
                    ProductQty = productQty,
                    ProductPrice = productPrice,
                    AddedDate = DateTime.Now
                };
                sql = @"INSERT INTO [dbo].[CartProducts]
                                   ([CartId]
                                   ,[ProductId]
                                   ,[ProductQty]
                                   ,[ProductPrice]
                                   ,[AddedDate])
                             VALUES
                                   (@CartId
                                   ,@ProductId
                                   ,@ProductQty
                                   ,@ProductPrice
                                   ,@AddedDate)";
                return SQLDataAccess.SaveData(sql, data);
            } 
        }

        public static List<CartProductsDataModel> LoadCartProducts(int userId)
        {
            CartDataModel model = GetOpenCart(userId);
            if (model != null) //Adding products to open cart
            {
                string sql = @"SELECT TOP (1000) [Id]
                                  ,[CartId]
                                  ,[ProductId]
                                  ,[ProductQty]
                                  ,[ProductPrice]
                                  ,[AddedDate]
                              FROM [dbo].[CartProducts] where [CartId]=" + model.Id;
                return SQLDataAccess.LoadData<CartProductsDataModel>(sql);
            }
            else { return null; }
        }

        public static int DeleteProductFromCart(int productId, int userId)
        {
            CartDataModel model = GetOpenCart(userId);
            if (model != null) //Adding products to open cart
            {
                CartProductsDataModel data = new CartProductsDataModel
                {
                    ProductId = productId,
                    CartId=model.Id
                };
                string sql = @"delete FROM [dbo].[CartProducts] where CartId=@CartId and ProductId=@ProductId";

                return SQLDataAccess.SaveData(sql, data);
            }
            else
            {
                return 0;
            }
        }

        public static int CloseCart(int userId)
        {
            CartDataModel model = GetOpenCart(userId);
            if (model != null) //Adding products to open cart
            {
                string sql = "UPDATE [dbo].[Cart] SET [IsOpen] = 0 WHERE [UserId]=@UserId";

                return SQLDataAccess.SaveData(sql, model);
            }
            else
            {
                return 0;
            }
        }
    }
}

using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class OrderProcessor
    {
        public static OrderDataModel GetOrder(int userId)
        {
            string sql = @" SELECT top 1
                               [Id]
                              ,[UserId]
                              ,[Name]
                              ,[CompanyName]
                              ,[Address]
                              ,[City]
                              ,[State]
                              ,[Zip]
                              ,[DateCreated]
                              ,[Status]
                              ,[TrackingNumber]
                          FROM [dbo].[Order] where UserId=" + userId +
                          " Order By [Id] Desc";

            return SQLDataAccess.GetRecord<OrderDataModel>(sql).FirstOrDefault();
        }

        public static OrderDataModel GetOrderById(int orderId)
        {
            string sql = @" SELECT top 1
                               [Id]
                              ,[UserId]
                              ,[Name]
                              ,[CompanyName]
                              ,[Address]
                              ,[City]
                              ,[State]
                              ,[Zip]
                              ,[DateCreated]
                              ,[Status]
                              ,[TrackingNumber]
                          FROM [dbo].[Order] where Id=" + orderId;

            return SQLDataAccess.GetRecord<OrderDataModel>(sql).FirstOrDefault();
        }

        public static int CreateNewOrder(int UserId, string Name, string CompanyName, string Address,
                                            string City, string State, string Zip, DateTime DateCreated)
        {
            //Saving Order Header
            OrderDataModel orderDataModel = new OrderDataModel
            {
                UserId = UserId,
                Name = Name,
                CompanyName = CompanyName,
                Address = Address,
                City = City,
                State = State,
                Zip = Zip,
                DateCreated = DateCreated
            };
            string sql = @"INSERT INTO [dbo].[Order]
                               ([UserId]
                               ,[Name]
                               ,[CompanyName]
                               ,[Address]
                               ,[City]
                               ,[State]
                               ,[Zip]
                               ,[DateCreated])
                         VALUES
                               (@UserId
                               ,@Name
                               ,@CompanyName
                               ,@Address
                               ,@City
                               ,@State
                               ,@Zip
                               ,@DateCreated)";
            SQLDataAccess.SaveData(sql, orderDataModel);

            OrderDataModel newOrder = GetOrder(UserId);
            CartDataModel openCart = CartProcessor.GetOpenCart(UserId);
            List<CartProductsDataModel> cartProducts = CartProcessor.LoadCartProducts(UserId);

            //Saving order details
            foreach (var product in cartProducts)
            {
                OrderProductsDataModel orderProducts = new OrderProductsDataModel
                {
                    OrderId = newOrder.Id,
                    ProductId = product.ProductId,
                    ProductDescription = ProductProcessor.GetProduct(product.ProductId).Description,
                    ProductQty = product.ProductQty,
                    ProductPrice = product.ProductPrice,
                    ProductImagePath = ProductProcessor.GetProduct(product.ProductId).ImagePath,
                    ProductCode = ""
                };
                sql = @"INSERT INTO [dbo].[OrderProducts]
                               ([OrderId]
                               ,[ProductId]
                               ,[ProductCode]
                               ,[ProductDescription]
                               ,[ProductImagePath]
                               ,[ProductQty]
                               ,[ProductPrice])
                         VALUES
                               (@OrderId
                               ,@ProductId
                               ,@ProductCode
                               ,@ProductDescription
                               ,@ProductImagePath
                               ,@ProductQty
                               ,@ProductPrice)";
                SQLDataAccess.SaveData(sql, orderProducts);
            }

            //Closing user cart
            CartProcessor.CloseCart(UserId);
            return newOrder.Id;
        }
        public static List<OrderDataModel> LoadOrders()
        {
            string sql = @"SELECT [Id]
                              ,[UserId]
                              ,[Name]
                              ,[CompanyName]
                              ,[Address]
                              ,[City]
                              ,[State]
                              ,[Zip]
                              ,[DateCreated]
                              ,[Status]
                              ,[TrackingNumber]
                          FROM [CigarStoreDB].[dbo].[Order]";

            return SQLDataAccess.LoadData<OrderDataModel>(sql);
        }

        public static List<OrderDataModel> LoadOrdersByUser(int userId)
        {
            string sql = @"SELECT [Id]
                              ,[UserId]
                              ,[Name]
                              ,[CompanyName]
                              ,[Address]
                              ,[City]
                              ,[State]
                              ,[Zip]
                              ,[DateCreated]
                              ,[Status]
                              ,[TrackingNumber]
                          FROM [CigarStoreDB].[dbo].[Order] where [UserId]=" + userId;

            return SQLDataAccess.LoadData<OrderDataModel>(sql);
        }

        public static List<OrderProductsDataModel> GetOrderProducts(int orderId)
        {
            string sql = @"SELECT [Id]
                              ,[OrderId]
                              ,[ProductId]
                              ,[ProductCode]
                              ,[ProductDescription]
                              ,[ProductImagePath]
                              ,[ProductQty]
                              ,[ProductPrice]
                          FROM [CigarStoreDB].[dbo].[OrderProducts] where [OrderId]="+orderId;

            return SQLDataAccess.LoadData<OrderProductsDataModel>(sql);
        }

        public static int SetOrderTrackingNo(int orderId, string trackingNo)
        {
            OrderDataModel orderDataModel = new OrderDataModel
            {
                Id = orderId,
                TrackingNumber=trackingNo,
                Status="Shipped"
            };
            string sql = @"UPDATE [dbo].[Order] SET Status=@Status, [TrackingNumber] = @TrackingNumber where Id=@Id";
            return SQLDataAccess.SaveData(sql, orderDataModel);
        }
    }
}

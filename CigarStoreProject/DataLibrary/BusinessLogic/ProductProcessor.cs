using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class ProductProcessor
    {
        public static int CreateProduct(string ProductCode, string Description, string UnitOfMeasure, string Category,
                                            float Price,string ImageTitle, string ImagePath, bool IsActive)
        {
            ProductDataModel data = new ProductDataModel
            {
                ProductCode = ProductCode,
                Description = Description,
                UnitOfMeasure = UnitOfMeasure,
                Category = Category,
                Price = Price,
                ImageTitle = ImageTitle,
                ImagePath = ImagePath,
                IsActive = IsActive
            };

            string sql = @"INSERT INTO [dbo].[Products]
                                ([ProductCode]
                                ,[Description]
                                ,[UnitOfMeasure]
                                ,[Category]
                                ,[Price]
                                ,[ImageTitle]
                                ,[ImagePath]
                                ,[IsActive])
                            VALUES (@ProductCode, @Description, @UnitOfMeasure, @Category, @Price, @ImageTitle,@ImagePath, @IsActive);";

            return SQLDataAccess.SaveData(sql, data);
        }

        public static List<ProductDataModel> LoadProducts()
        {
            string sql = @"SELECT [Id]
                      ,[ProductCode]
                      ,[Description]
                      ,[UnitOfMeasure]
                      ,[Category]
                      ,[Price]
                      ,[ImageTitle]
                      ,[ImagePath]
                      ,[IsActive]
                  FROM [dbo].[Products] where [IsActive]=1";

            return SQLDataAccess.LoadData<ProductDataModel>(sql);
        }

        public static List<ProductDataModel> LoadProductsAdmin()
        {
            string sql = @"SELECT [Id]
                      ,[ProductCode]
                      ,[Description]
                      ,[UnitOfMeasure]
                      ,[Category]
                      ,[Price]
                      ,[ImageTitle]
                      ,[ImagePath]
                      ,[IsActive]
                  FROM [dbo].[Products]";

            return SQLDataAccess.LoadData<ProductDataModel>(sql);
        }

        public static ProductDataModel GetProduct(int id)
        {
            string sql = @" SELECT [Id]
                              ,[ProductCode]
                              ,[Description]
                              ,[UnitOfMeasure]
                              ,[Category]
                              ,[Price]
                              ,[ImageTitle]
                              ,[ImagePath]
                              ,[IsActive]
                          FROM [CigarStoreDB].[dbo].[Products] where Id=" + id;

            return SQLDataAccess.GetRecord<ProductDataModel>(sql).First();
        }

        public static int UpdateProduct(int Id,string ProductCode, string Description, string UnitOfMeasure, string Category,
                                            float Price, string ImageTitle, string ImagePath, bool IsActive)
        {
            ProductDataModel data = new ProductDataModel
            {
                ProductCode = ProductCode,
                Description = Description,
                UnitOfMeasure = UnitOfMeasure,
                Category = Category,
                Price = Price,
                ImageTitle = ImageTitle,
                ImagePath = ImagePath,
                IsActive = IsActive
            };

            string sql = @"UPDATE [dbo].[Products]
                           SET [ProductCode] = @ProductCode
                              ,[Description] = @Description
                              ,[UnitOfMeasure] = @UnitOfMeasure
                              ,[Category] = @Category
                              ,[Price] = @Price
                              ,[ImageTitle] = @ImageTitle
                              ,[ImagePath] = @ImagePath
                              ,[IsActive] = @IsActive
                         WHERE Id=" + Id;

            return SQLDataAccess.SaveData(sql, data);
        }

        public static List<ProductDataModel> LoadProductsByDescription(string searchValue)
        {
            string sql = @"SELECT [Id]
                      ,[ProductCode]
                      ,[Description]
                      ,[UnitOfMeasure]
                      ,[Category]
                      ,[Price]
                      ,[ImageTitle]
                      ,[ImagePath]
                      ,[IsActive]
                  FROM [dbo].[Products] where [IsActive]=1 and  [ProductCode] like '%" + searchValue + "%' or [Description] like '%"+ searchValue +"%'";

            return SQLDataAccess.LoadData<ProductDataModel>(sql);
        }

        public static List<ProductDataModel> LoadProductsByPriceRange(float minPrice, float maxPrice)
        {
            string sql = @"SELECT [Id]
                      ,[ProductCode]
                      ,[Description]
                      ,[UnitOfMeasure]
                      ,[Category]
                      ,[Price]
                      ,[ImageTitle]
                      ,[ImagePath]
                      ,[IsActive]
                  FROM [dbo].[Products] where [IsActive]=1 and [Price] between " + minPrice + "and  " + maxPrice;

            return SQLDataAccess.LoadData<ProductDataModel>(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser(string FirstName, string LastName, string CompanyName, string TaxId,
            string TobaccoLicense, string EmailAddress, string Password, string Address1, string Address2,
            string City, string State, string Zip, bool IsActive, bool IsAdmin)
        {
            UserDataModel data = new UserDataModel
            {
                FirstName = FirstName,
                LastName = LastName,
                CompanyName = CompanyName,
                TaxId = TaxId,
                TobaccoLicense = TobaccoLicense,
                EmailAddress = EmailAddress,
                Password = Password,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                Zip = Zip,
                IsActive=IsActive,
                IsAdmin= IsAdmin
            };

            string sql = @"insert into dbo.UserTable (FirstName, LastName, CompanyName, TaxId, TobaccoLicense, EmailAddress, Password,
                           Address1, Address2, City, State, Zip, IsActive,IsAdmin)
                           values (@FirstName, @LastName, @CompanyName, @TaxId, @TobaccoLicense, @EmailAddress, @Password,
                                    @Address1, @Address2, @City, @State, @Zip, @IsActive,@IsAdmin);";

            return SQLDataAccess.SaveData(sql, data);
        }

        public static List<UserDataModel> LoadUsers()
        {
            string sql = @"select Id, FirstName, LastName, CompanyName, TaxId, TobaccoLicense,
                          EmailAddress, Password, Address1, Address2, City, State, Zip, IsActive, IsAdmin
                          from dbo.UserTable";

            return SQLDataAccess.LoadData<UserDataModel>(sql);
        }

        public static UserDataModel GetUser(int userId)
        {
            string sql = @"select Id, FirstName, LastName, CompanyName, TaxId, TobaccoLicense,
                          EmailAddress, Password, Address1, Address2, City, State, Zip,IsActive, IsAdmin
                          from dbo.UserTable where Id=" + userId;

            return SQLDataAccess.GetRecord<UserDataModel>(sql).First();
        }

        public static UserDataModel GetUserByEmail(string emailAddress)
        {
            string sql = @"select Id, FirstName, LastName, CompanyName, TaxId, TobaccoLicense,
                          EmailAddress, Password, Address1, Address2, City, State, Zip,IsActive, IsAdmin
                          from dbo.UserTable where EmailAddress='" + emailAddress + "'";

            return SQLDataAccess.GetRecord<UserDataModel>(sql).First();
        }

        public static int UpdateUser(int Id,string FirstName, string LastName, string CompanyName, string TaxId,
            string TobaccoLicense, string EmailAddress, string Password, string Address1, string Address2,
            string City, string State, string Zip, bool IsActive)
        {
            UserDataModel data = new UserDataModel
            {
                Id=Id,
                FirstName = FirstName,
                LastName = LastName,
                CompanyName = CompanyName,
                TaxId = TaxId,
                TobaccoLicense = TobaccoLicense,
                EmailAddress = EmailAddress,
                Password = Password,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                Zip = Zip,
                IsActive=IsActive
            };

            string sql = @"update dbo.UserTable set FirstName=@FirstName, LastName=@LastName,CompanyName=@CompanyName, 
                            TaxId=@TaxId, TobaccoLicense=@TobaccoLicense, EmailAddress=@EmailAddress, Password=@Password,
                           Address1=@Address1, Address2=@Address2, City=@City, State=@State, Zip=@Zip, IsActive=@IsActive
                           where Id=@Id;";

            return SQLDataAccess.SaveData(sql, data);
        }
    }
}

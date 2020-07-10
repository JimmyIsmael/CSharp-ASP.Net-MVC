using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class StoreViewModel
    {
        public List<CartProductsModel> CartProducts { get; set; }
        public List<ProductModel> StoreProducts { get; set; }
    }
}
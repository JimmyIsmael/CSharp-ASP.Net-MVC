using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class OrderProductsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImagePath { get; set; }
        public int ProductQty { get; set; }
        public float ProductPrice { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class CartProductsModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImagePath { get; set; }
        public int ProductQty { get; set; }
        public float ProductPrice { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
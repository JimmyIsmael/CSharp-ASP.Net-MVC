using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class CartProductsDataModel
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

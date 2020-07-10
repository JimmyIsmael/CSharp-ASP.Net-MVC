using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ProductDataModel
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

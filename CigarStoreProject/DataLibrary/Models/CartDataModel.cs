using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class CartDataModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsOpen { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

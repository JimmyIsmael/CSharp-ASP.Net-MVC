using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsOpen { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
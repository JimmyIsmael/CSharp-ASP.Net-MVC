using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class CompleteOrderModel
    {
        public OrderModel OrderHeader { get; set; }
        public List<OrderProductsModel> OrderDetails { get; set; }
    }
}
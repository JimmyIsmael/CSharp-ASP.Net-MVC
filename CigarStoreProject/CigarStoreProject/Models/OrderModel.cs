using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Ordered By")]
        public string Name { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [Display(Name = "Order Date")]
        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }
    }
}
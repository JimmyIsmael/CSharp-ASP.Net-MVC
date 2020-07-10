using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CigarStoreProject.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "Product code is required.")]
        public string ProductCode { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Display(Name = "Unit of Measure")]
        [Required(ErrorMessage = "Unit of measure is required.")]
        public string UnitOfMeasure { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required.")]
        public float Price { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Image Title")]
        public string ImageTitle { get; set; }

        [Display(Name = "Path")]
        public string ImagePath { get; set; }

        [Display(Name = "Select a File")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
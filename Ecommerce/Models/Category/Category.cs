using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0,100)]
        public int Displayorder { get; set; }
        //for dropdownlist
        [Display(Name = "Category")]
        public string SelectedCategoryName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Product
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Count { get; set; }
        
        public string Category{ get; set; }
        //to take image as input
        public HttpPostedFileBase ImageData { get; set; }
        //for iamge
        public byte[] ImageDataBytes { get; set; }

    }
}
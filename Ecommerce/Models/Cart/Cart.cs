using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Cart
{
    public class Cart
    {
        public int Id { get; set; }
        public byte[] ImageDataBytes { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public  string Count { get; set; }
        public int Price { get; set; }
    }
}
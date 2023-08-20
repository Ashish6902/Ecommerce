using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Purchased
{
    public class Orders
    {
        public int OrderId { get; set; }
        public string FName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string address { get; set; }
        public string ProductName { get; set; }
        public byte[] ImageDataBytes { get; set; }
    }
} 
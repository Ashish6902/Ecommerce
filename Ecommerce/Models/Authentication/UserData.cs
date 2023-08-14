using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.Authentication
{
    public class UserData
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public  string Fname  { get; set; }
        [Required]
        public  string Lname  { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public  string PhoneNo { get; set; }
        public  string Address  { get; set; }
        public string Role { get; set; }

    }
}
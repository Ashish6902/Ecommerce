//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce
{
    using System;
    using System.Collections.Generic;
    
    public partial class Logins
    {
        public int Login_id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<long> PhoneNo { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public string BrandName { get; set; }
        public Nullable<int> Role_id { get; set; }
    
        public virtual Roles Roles { get; set; }
    }
}
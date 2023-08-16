using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models.EntityAuthentication.DBoperations
{
    public class LoginRepository
    {
        public int Adduser(Logins model)
        {
            using (var context = new EcommerceEntities())
            {
                Logins log = new Logins() 
                { 
                    UserName = model.UserName,
                    HashedPassword = model.HashedPassword,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNo = model.PhoneNo,
                    Email = model.Email,
                    UserAddress = model.UserAddress,
                    BrandName = model.BrandName,
                    Role_id = model.Role_id

                };
                
                context.Logins.Add(log);
                context.SaveChanges();
                return model.Login_id;
            }
        }
    }
}
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
                    Roles = new Roles()
                    {
                        Roles1 = "User"
                    }
                };
                
                context.Logins.Add(log);
                context.SaveChanges();
                return model.Login_id;
            }
        }

        public int AddSeller(Logins model)
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
                    Roles = new Roles()
                    {
                        Roles1 = "Seller"
                    }
                };

                context.Logins.Add(log);
                context.SaveChanges();
                return log.Login_id;
            }
        }

        public int AddAdmin(Logins model)
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
                    Roles = new Roles()
                    {
                        Roles1 = "Admin"
                    }
                };

                context.Logins.Add(log);
                context.SaveChanges();
                return log.Login_id;
            }
        }
    }
}
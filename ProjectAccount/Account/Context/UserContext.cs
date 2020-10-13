using Account.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Account.Context
{
    public class UserContext:DbContext
    {
        public UserContext()
              : base("name=Context") 
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
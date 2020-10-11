using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Questionary.Context
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
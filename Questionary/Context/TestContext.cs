using Questionary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Questionary.Context
{
    public class TestContext:DbContext
    {
        public TestContext()
            : base("name=Context") 
        {
        }
        public DbSet<TestForUser> Tests { get; set; }
    }
}
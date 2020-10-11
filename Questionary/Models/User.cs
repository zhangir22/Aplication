using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    [Table("Clients")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name="Last name")]
        public string LastName { get; set; }
        public int Age { get; set; }

    }
}
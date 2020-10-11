using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    [Table("Test")]
    public class TestForUser
    {
        public int Id { get; set; }
        [Display(Name = "Date complete")]
        public DateTime DateComplete { get; set; }
        [Display(Name = "Capital Kazakhstan")]
        public string CapitalKZ { get; set;}
        [Display(Name = "Count city Kazakhstan")]
        public int CountCityKZ { get; set;}
        [Display(Name = "Language Kazakhstan")]
        public string LanguageKZ { get; set;}
        [DataType(DataType.Date)]
        [Display(Name = "Data founded")]
        public DateTime DateFounded { get; set; }
        public int ClientsId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionary.Models
{
    public class ResultUser
    {
        public ResultUser() 
        {
        }
        [DataType(DataType.Date)]
        public DateTime DateComplete { get; set; }
        public int RightAnswer { get; set; }
        public int LieAnswer { get; set; }
        public string Name { get; set; }
        public int Porsent { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcTaskModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="This Feild Required When Submitting")]
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
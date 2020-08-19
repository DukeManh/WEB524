using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstWebApp.Models
{
    public class EmployeeBaseViewModel : EmployeeAddViewModel 
    {
        [Key] //It refers to a primary key
        public int EmployeeId { get; set; }

        
    }


}
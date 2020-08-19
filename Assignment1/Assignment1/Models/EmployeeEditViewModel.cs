using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class EmployeeEditViewModel
    {
        public int EmployeeId { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [StringLength(24, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Range(1, 4)]
        [Display(Name = "Vacation Length")]
        public int VacationLength { get; set; }



    }
}
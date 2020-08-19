using Assignment2.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class InvoiceBaseViewModel
    {
        [Key]
        [Display(Name = "Invoice number")]
        public int InvoiceId { get; set; }

        [Display(Name = "Invoice date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        

        [StringLength(70)]
        [Display(Name = "Billing address")]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        [Display(Name = "Billing city")]
        public string BillingCity { get; set; }

        [StringLength(40)]
        [Display(Name = "Billing state")]
        public string BillingState { get; set; }

        [StringLength(40)]
        [Display(Name = "Billing country")]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal code")]
        public string BillingPostalCode { get; set; }

        [Column(TypeName = "numeric")]
        [Display(Name = "Invoice total")]
        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
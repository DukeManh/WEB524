using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class InvoiceWithDetailViewModel : InvoiceBaseViewModel
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerEmployeeFirstName { get; set; }
        public string CustomerEmployeeLastName { get; set; }

        public IEnumerable<InvoiceLineWithDetailViewModel> InvoiceLines { get; set; }

    }
}
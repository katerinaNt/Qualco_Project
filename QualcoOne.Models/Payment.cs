using System;
using System.Collections.Generic;
using System.Text;

namespace QualcoOne.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Paymentamount { get; set; }
	    public DateTime PaymentDateTime { get; set; }

        //public int? BillId { get; set; }
        public virtual Bill Bill { get; set; }                
    }
}
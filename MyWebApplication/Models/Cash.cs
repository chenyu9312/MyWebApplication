using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Models
{
    public enum Payment : int
    {
        Cash = 1, Bank =2
    }
    public enum CurrencyCode : int
    {
        CAD = 1, USD = 2
    }
    public class Cash
    {
        [Display(Name = "Receipt ID")]
        public int ID { get; set; }


        public int ReceiptNumber { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Required]
        public decimal Amount { get; set; }

        [Required]

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(150)]
        public string Remarks { get; set; }

        [Display(Name = "Receipt Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceiptDate { get; set; }

        public Payment? Payment { get; set; }
        public CurrencyCode? CurrencyCode { get; set; }


        public int BranchID { get; set; }

        public Branch Branch { get; set; }


    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime OrderDate { get; set; } // ngay dat
        public DateTime ShippingDate { get; set; } // ngaygiao
        public double OrderTotal { get; set; } // Tong don dat hang
        public string? OrderStatus { get; set; } // trang thai
        public string? PaymentStatus { get; set; } // trang thai thanh toan
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        // danh cho company co the thanh toan trong 30 ngay tam bo
/*        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }*/
        public string? PaymentIntentId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

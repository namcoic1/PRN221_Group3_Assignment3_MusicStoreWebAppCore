using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SE1611_Group3_A3.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public string? PromoCode { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public decimal? Total { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

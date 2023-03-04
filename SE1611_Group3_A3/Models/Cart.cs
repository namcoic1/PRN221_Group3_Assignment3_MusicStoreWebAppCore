using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SE1611_Group3_A3.Models
{
    public partial class Cart
    {
        public int RecordId { get; set; }
        public string CartId { get; set; } = null!;
        public int AlbumId { get; set; }
        public int Count { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public virtual Album Album { get; set; } = null!;

        public Cart(string cartId, int albumId, int count, DateTime dateCreated)
        {
            CartId = cartId;
            AlbumId = albumId;
            Count = count;
            DateCreated = dateCreated;
        }
    }
}

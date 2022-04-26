using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models
{
    public class StackItem
    {
        [Key]
        public string ItemId { get; set; }

        public string StackId { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateTaking { get; set; }

        public int BookId { get; set; }

        public virtual Book BookItem { get; set; }
    }
}
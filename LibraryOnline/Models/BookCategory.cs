using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models
{
    public class BookCategory
    {
        [ScaffoldColumn(false)]
        public int BookCategoryId { get; set; }

        [Required, StringLength(100), Display(Name = "CategoryName")]
        public string BookCategoryName { get; set; }


    }
}
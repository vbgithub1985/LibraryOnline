using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryOnline.Models
{
    public class Book
    {
        [ScaffoldColumn(false)]
        public int BookId { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string BookName { get; set; }

        [Required, StringLength(100), Display(Name = "Author")]
        public string BookAuthor { get; set; }

        [Display(Name = "Summary")]
        public string Summary { get; set; }

        public string ImagePath { get; set; }

        public int BookCount { get; set; }

        public int? BookCategoryId { get; set; }

        public virtual BookCategory Category { get; set; }
    }
}
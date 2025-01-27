﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Models
{
    public class News
    {
        public int Id { get; set; }

        [Display(Name = "News title")]
        [Required(ErrorMessage = "Error, invalid title!!!")]
        [RegularExpression("^[A-Z][a-z]{1,99}$", ErrorMessage = "RegularExpression error!")]
        [MinLength(8, ErrorMessage = "Error, min lenght 8 letters!")]
        [MaxLength(100, ErrorMessage = "Error, max length 100 letters!")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        
        
        
        [Display(Name = "News content text")]
        [Required(ErrorMessage = "Error, invalid content!!!")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        //[Display(Name = "Url for poster")]
        //[DataType(DataType.Upload, ErrorMessage = "Incorrect image url!")]
        public string PosterUrl { get; set; }
        
        
        [Display(Name ="Category")]
        [Required(ErrorMessage = "Error, invalid category!!!")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime Date { get; set; }

        public virtual IEnumerable<NewsTag> NewsTags { get; set; }
    }
}

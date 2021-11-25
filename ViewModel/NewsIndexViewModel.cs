using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.ViewModel
{
    public class NewsIndexViewModel
    {
        public IEnumerable<News> News {get;set;}
        public IEnumerable<Category> Categories { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int CategoryId { get; set; }
    }
}

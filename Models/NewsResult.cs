using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWeb.Models
{
    public class NewsResult
    {
        public virtual IEnumerable<News> News { get; set; }
        public double TotalPages { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockWebFramework.Models
{
    internal class Book
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }

        public double? Price { get; set; }
    }
}

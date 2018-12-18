using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventWatcher.Models
{
    public class OperaDetails
    {

        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Seats { get; set; }
        public DateTime? EventDate { get; set; }


        public string OperaTitle { get; set; }
        public string MainActor { get; set; }
        public string SecondActor { get; set; }
        public long Period { get; set; }
        public string Popularity { get; set; }
        

    }
}
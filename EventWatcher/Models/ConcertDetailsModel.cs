using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventWatcher.Models
{
    public class ConcertDetails
    {
       
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Seats { get; set; }
        public DateTime? EventDate { get; set; }

        public string BandName { get; set; }
        public string Theme { get; set; }
        public long Period { get; set; }
        public  string Popularity { get; set; }
       
    }
}
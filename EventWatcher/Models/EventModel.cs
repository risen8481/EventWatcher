using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventWatcher.Models
{
    public class EventModel
    {
        public long EventId { get; set; }
        public string EventType { get; set; }
        public string Location { get; set; }
        public DateTime? EventDate { get; set; }
        public string ShowingDay { get; set; }
        public string ShowingPrice { get; set; }
        public decimal Price { get; set; }
        public string Descryption { get; set; }
    }
}
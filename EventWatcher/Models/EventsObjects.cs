using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventWatcher.Models
{
    public class EventsObjects
    {
        public long EventType { get; set; }
        public List<object> EventObjectList { get; set; }

    }
}
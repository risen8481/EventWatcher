using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventWatcher.Models
{
    public class AuthResult
    {
        public int Status { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
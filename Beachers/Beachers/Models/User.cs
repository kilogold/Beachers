using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Models
{
    public class User
    {
        public string name { get; set; }
        public string phone { get; set; }

        public override string ToString()
        {
            return $"Name: {name} | Phone: {phone}";
        }
    }
}

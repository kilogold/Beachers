using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Models
{
    public enum GearType
    {
        Kite,
        Board
    }

    public class GearModel
    {
        public string Brand { get; set; }
        public string Memo { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public GearType Type { get; set; }
    }
}

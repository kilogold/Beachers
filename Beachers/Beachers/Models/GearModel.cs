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
        public string brand;
        public string memo;
        public string size;
        public GearType type;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Models
{
    public class BookingModel
    {
        public string beacherId;
        public string[] gear;
        public int[][] deployments;
        public int sessionLengthMinutes;
        public float[] location;
    }
}

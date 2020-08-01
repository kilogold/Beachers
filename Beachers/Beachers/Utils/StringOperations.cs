using System;
using System.Collections.Generic;
using System.Text;

namespace Beachers.Utils
{
    public static class StringOperations
    {
        public static string ToUTCFormat(DateTime utcDateTime)
        {
            return utcDateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
        }
    }
}

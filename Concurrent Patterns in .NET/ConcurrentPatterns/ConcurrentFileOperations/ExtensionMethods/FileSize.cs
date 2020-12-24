using System;

namespace ConcurrentFileOperations.ExtensionMethods
{
    public static class FileSize
    {
        public enum Units
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public static string ToSize(this Int64 value, Units unit)
        {
            return (value / Math.Pow(1024, (Int64)unit)).ToString($"#,##0 {unit}");
        }

        //Will not work for anything larger than Petabytes
        public static string ToSize(this Int64 value)
        {
            string retVal = string.Empty;
            
            if (value > 0 && value < 1024)
            {
                retVal = (value / Math.Pow(1024, (Int64)Units.Byte)).ToString($"#,##0 {Units.Byte}");
            }
            if(value >= 1024 && value < 1048576)
            {
                retVal = (value / Math.Pow(1024, (Int64)Units.KB)).ToString($"#,##0 {Units.KB}");
            }
            if (value >= 1048576 && value < 1073741824)
            { 
                retVal = (value / Math.Pow(1024, (Int64)Units.MB)).ToString($"#,##0 {Units.MB}");
            }
            if (value >= 1073741824 && value < 1099511627776)
            {
                retVal = (value / Math.Pow(1024, (Int64)Units.GB)).ToString($"#,##0 {Units.GB}");
            }

            if (value >= 1099511627776 && value < 1125899906842624)
            {
                retVal = (value / Math.Pow(1024, (Int64)Units.TB)).ToString($"#,##0 {Units.TB}");
            }
            if (value >= 1125899906842624 && value < 1152921504606846976)
            {
                retVal = (value / Math.Pow(1024, (Int64)Units.PB)).ToString($"#,##0 {Units.PB}");
            }
           
            return retVal;
        }
    }
}

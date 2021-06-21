using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionModule
{
    public class DateTimeModule
    {
        private static DateTime _utcStartTime= new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetNowTimeStamp()
        {
            double nowTimestamp = (DateTime.UtcNow - _utcStartTime).TotalMilliseconds;
            return (long)nowTimestamp;
        }
    }
}

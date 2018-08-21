using System;

namespace ERP.Shared
{
    public static class DateTimeExtension
    {
        public static string ToDate(this DateTime data)
        {
            return data.Date.ToString("dd/MM/yyyy");
        }
    }
}
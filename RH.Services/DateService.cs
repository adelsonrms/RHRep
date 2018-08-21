using System;

namespace RH.Services
{
   public class DateService
    {
        private int _years, _months, _days;

        public int Years
        {
            get { return _years; }
        }
        public int Months
        {
            get { return _months; }
        }
        public int Days
        {
            get { return _days; }
        }

        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }

        public DateService()
        {
                
        }

        public DateService(DateTime startDate, DateTime endDate)
        {
            DtInicial = startDate;
            DtFinal = endDate;
        }

        public string TempoDecorrido(string formato)
        {
            return TempoDecorrido(DtInicial, DtFinal, formato);
        }

        public string TempoDecorrido(DateTime startDate, DateTime endDate, string formato)
        {
            DtInicial = startDate;
            DtFinal = endDate;

            //for simplicity, let's keep the TimeSpan2 to positive time spans
            if (startDate > endDate)
            {
                DateTime tmpSwap = startDate;
                startDate = endDate;
                endDate = tmpSwap;
            }


            int startYear = startDate.Year;
            int startMonth = startDate.Month;
            int startDay = startDate.Day;


            int endYear = endDate.Year;
            int endMonth = endDate.Month;
            int endDay = endDate.Day;


            //perform the date math by subtracting startdate from enddate
            //we actually subtract using the individual y/m/d pieces
            //borrowing from the left as needed to avoid going negative...


            // working on the 1's / day's column
            if (endDay < startDay)
            {
                //borrow days from months column
                //use previous month so we can see exactly how many days it actually has
                DateTime previousMonth = endDate.AddMonths(-1);
                endDay += DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month);


                //decrement our endmonth number since we just borrowed a month
                endMonth -= 1;


                //watch for invalid month and borrow from the years column if needed
                if (endMonth < 1)
                {
                    endMonth += 12;
                    endYear -= 1;
                }
            }


            // working on the 10's / month's column
            if (endMonth < startMonth)
            {
                //borrow months from the years column
                endMonth += 12;
                endYear -= 1;
            }


            _years = endYear - startYear;
            _months = endMonth - startMonth;
            _days = endDay - startDay;

            //build up date parts and pluralize as needed
            const string plural = "s";

            //years and months not shown if they are zero but days are.
            string yearString = (Years == 0 ? string.Empty : string.Format("{0} Ano{1}", Years, (Years > 1 ? plural : string.Empty))).ToString();
            string monthString = (Months == 0 ? string.Empty : string.Format("{0} Mes{1}", Months, (Months > 1 ? "es" : string.Empty))).ToString();
            string dayString = string.Format("{0} Dia{1}", Days, (Days != 1 ? plural : string.Empty));

            if (formato == "y")
            {
                return string.Format("{0}", yearString);
            }
            else
            {
                if (formato == "ym")
                {
                    return string.Format("{0} e {1}", yearString, monthString);
                }
                else
                {
                    return string.Format("{0}, {1} e {2}", yearString, monthString, dayString);
                }
            }
        }
    }



}

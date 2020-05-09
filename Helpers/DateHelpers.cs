using System;

namespace TreinoAPI.Helpers
{
    public class DateHelpers
    {

        public DateTime GetNextDateForDay(int IDSemanaDia)
        {
            // (There has to be a better way to do this, perhaps mathematically.)
            // Traverse this week
            DateTime nextDate = DateTime.Now;
            DayOfWeek desiredDay = GetDayOfWeekPorIDSemanaDia(IDSemanaDia);
            while (nextDate.DayOfWeek != desiredDay)
                nextDate = nextDate.AddDays(1D);

            return nextDate;
        }


        private  DayOfWeek GetDayOfWeekPorIDSemanaDia(int IDSemanaDia)
        {
            if(IDSemanaDia == 1)
            {
                return DayOfWeek.Sunday;
            }
            else if(IDSemanaDia == 2)
            {
                return DayOfWeek.Monday;
            }

            else if (IDSemanaDia == 3)
            {
                return DayOfWeek.Tuesday;
            }

            else if(IDSemanaDia == 4)
            {
                return DayOfWeek.Wednesday;
            }

            else if(IDSemanaDia == 5)
            {
                return DayOfWeek.Thursday;
            }

            else if(IDSemanaDia == 6)
            {
                return DayOfWeek.Friday;
            }

            return DayOfWeek.Saturday;

        }
    }
}

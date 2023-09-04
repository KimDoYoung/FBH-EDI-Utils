using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public class CommonUtil
    {
        public static void PrintDataTable(DataTable table)
        {
            if(table == null)
            {
                System.Diagnostics.Debug.WriteLine("table is null");
                return;
            }
            int r = 0;
            int c = 0;
            foreach (DataRow dataRow in table.Rows)
            {
                c = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    System.Diagnostics.Debug.WriteLine($"{r},{c} : {item}");
                    c++;
                }
                r++;
            }

        }

        public static void Console(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public static void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        /// <summary>
        /// random 파일명을 만들고 postfix를 붙인다
        /// </summary>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string RandomFilename(string postfix)
        {
            var name = Path.GetRandomFileName();
            return name.Substring(0, 8) + "_" + postfix;
        }

        public static decimal ToDecimal(object o)
        {

            if (o == null || o.ToString().Length < 1)
            {
                throw new EdiException("잘못된 문자를 숫자로 변환하려고 합니다.");
            }
            try
            {
                return Convert.ToDecimal(o);
            }
            catch (Exception e)
            {

                throw new EdiException("잘못된 문자를 숫자로 변환하려고 합니다. " + e.Message);
            }
        }

        public static int ToInteger(object o)
        {
            if (o == null || o.ToString().Length < 1)
            {
                throw new EdiException("잘못된 문자를 숫자로 변환하려고 합니다.");
            }
            try
            {
                return Convert.ToInt32(o);
            }
            catch (Exception e)
            {

                throw new EdiException("잘못된 문자를 숫자로 변환하려고 합니다. " + e.Message);
            }
        }
        /// <summary>
        /// 주차(週次) 구하기
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime sourceDate, CultureInfo cultureInfo, DayOfWeek dayOfWeek)
        {
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }

            //해당 주의 첫째 요일 전까지 4일 이상이 있는 첫째 주가 해당 연도의 첫째 주가 되도록 지정
            CalendarWeekRule calendarWeekRule = CalendarWeekRule.FirstFourDayWeek;
            //주의 시작요일이 일요일 또는 월요일인지 확인. dayOfWeek가 일/월이 아닌 경우 월요일로 설정
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Monday)
            {
                firstDayOfWeek = dayOfWeek;
            }
            else firstDayOfWeek = DayOfWeek.Monday;

            int WeekOfYear = cultureInfo.Calendar.GetWeekOfYear(sourceDate, calendarWeekRule, firstDayOfWeek);

            return WeekOfYear;
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}

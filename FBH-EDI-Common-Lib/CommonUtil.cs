using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FBH.EDI.Common
{
    public class CommonUtil
    {
        public static void PrintDataTable(System.Data.DataTable table)
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
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
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

        public static string IfEmpty(string s, string dftValue)
        {
            if(s == null || s.Trim().Length == 0)
            {
                return dftValue;
            }
            return s;
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

        public static object YmdFormat(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            string t = Regex.Replace(s, @"\D", "");
            if (t.Length < 8) return s;

            return t.Substring(0, 4) + "-" + t.Substring(4, 2) + "-" + t.Substring(6,2);
        }
        /// <summary>
        /// M/DD/YYYY형태의 날짜 스트링을 YMD(2023-08-02) 형태로 변경한다.
        /// </summary>
        /// <param name="mdy">8/2/2023 형태의 문자</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string MdyToYmd(string mdy)
        {
            if (string.IsNullOrEmpty(mdy)) return "";

            string[] mdyArray = mdy.Split('/');
            if(mdyArray.Length == 3) {
                int m = Convert.ToInt16(mdyArray[0]);
                int d = Convert.ToInt16(mdyArray[1]);
                var year = mdyArray[2]; //시간까지 있을 수 있다.
                if (mdyArray[2].Length > 4)
                {
                    year = mdyArray[2].Substring(0, 4);
                }
                int y = Convert.ToInt16(year);
                
                var dt  = new DateTime(y, m, d, 0, 0,0);
                return dt.ToString("yyyy-MM-dd");
            }
            return mdy;
        }

        /// <summary>
        /// s가 null or empty이면 null로 아니면 integer로 변환한다.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static int? ToIntOrNull(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return null;
            }
            return Convert.ToInt32(s);
        }
        /// <summary>
        /// s가 null or empty이면 null을 리턴 아니면 decimal로 convert
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static decimal? ToDecimalOrNull(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            return Convert.ToDecimal(s);
        }

        internal static bool IsValidCellValue(object o)
        {
            if (o == null) return false;
            string value = o.ToString();
            if (value.Trim().Length < 1) return false;
            return true;
        }

        /// <summary>
        /// 숫자아닌 문자를 제거한다.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string OnlyNum(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return Regex.Replace(s, @"\D","");
        }
        /// <summary>
        /// string s가 숫자문자열로만 되어 있는지 판단한다.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsOnlyNum(string s)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(s);
        }

        /// <summary>
        /// 06-08-2023 형태의 날짜를 20230608 로 리턴함.
        /// </summary>
        /// <param name="s">MM-dd-YYYY형태의 문자열</param>
        /// <returns>yyyy</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string MmddyyyyToYmd(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string[] tmp = s.Split('-');
            if (tmp.Length == 3)
            {
                return tmp[2] + tmp[0] + tmp[1];
            }
            return s;
        }
    }
}

using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace EdiUtils
{
    internal class WeekOfYearKroger
    {
        class DateAndChasu
        {
            public DateAndChasu(DateTime startDate, int chasu)
            {
                this.StartDate = startDate;
                this.Chasu = chasu;
            }
            public DateTime StartDate { get; set; }
            public int Chasu { get; set; }
        }
        List<DateAndChasu> list = null;
        public WeekOfYearKroger()
        {
            var strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            var dateFilePath = $"{strWorkPath}/WeekOfYear-Kroger.csv";
            if(File.Exists(dateFilePath) == false)
            {
                throw new EdiUtilsException("WeekOfYear-Kroger.csv 파일이 실행파일이 있는 폴더에 없습니다");
            }

            list = new List<DateAndChasu>();
            var lines = File.ReadAllLines(dateFilePath, Encoding.UTF8);
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("#")) continue;
                if (line.Trim().Length < 1) continue;

                string[] tmp = line.Trim().Split(',');
                if (tmp.Length != 2) continue;
                if (tmp[0].Trim().Length != 10) continue;
                if (tmp[1].Trim().Length < 1) continue;
                DateTime startDate = Convert.ToDateTime(tmp[0].Trim());
                int chasu = Convert.ToInt32(tmp[1].Trim());
                list.Add(new DateAndChasu(startDate, chasu));
            }
        }
        public int GetWeekOfYear(DateTime date)
        {
            foreach (var item in list)
            {
                DateTime startDate = item.StartDate;
                if( startDate <= date && date <= startDate.AddDays(6))
                {
                    return item.Chasu;
                }
            }
            return -1;
        }
        public int GetWeekOfYear(string date)
        {
            string sDate = Regex.Replace(date, "\\D","");
            if (sDate.Length == 8)
            {
                string s = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6);
                return this.GetWeekOfYear(Convert.ToDateTime(s));
            }
            else
            {
                return -1;
            }
        }

    }
}
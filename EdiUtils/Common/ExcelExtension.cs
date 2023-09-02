using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace EdiUtils.Common
{
    public static class ExcelExtension
    {
        private static int GetCol(string colName)
        {
            int iCol = -1;
            if (colName.Length == 1)
            {
                iCol = Convert.ToChar(colName) - 'A' + 1;
            }
            else if (colName.Length == 2)
            {
                iCol = ((Convert.ToChar(colName[0]) - 'A' + 1) * 26) + (Convert.ToChar(colName[1]) - 'A') + 1;
            }
            else
            {
                throw new Exception("{col} 은 row로 환산될 수 없습니다.");
            }
            return iCol;

        }

        private static RowCol<int, int> GetRowCol(string cellName)
        {
            string cellNamePattern = @"([A-Z]{1,2})([0-9]{1,2})";
            Regex r = new Regex(cellNamePattern, RegexOptions.IgnoreCase);

            Match m = r.Match(cellName);
            string row = "";
            string col = "";
            if (m.Success)
            {
                if (m.Groups.Count == 3)
                {
                    Group gCol = m.Groups[1];
                    Group gRow = m.Groups[2];
                    col = gCol.ToString();
                    row = gRow.ToString();
                }
            }
            if (row.Length > 0 && col.Length > 0)
            {
                int indexRow = Convert.ToInt32(row);
                int indexCol = GetCol(col);
                return new RowCol<int, int>(indexRow, indexCol);
            }
            return null;

        }

        public static void SetCell(this Excel.Worksheet workSheet, int row, int col, object o, string format=null)
        {

            if (string.IsNullOrEmpty(format) == false)
            {
                workSheet.Cells[row, col].NumberFormat = format;
            }
            workSheet.Cells[row, col] = o;
        }

        public static void SetCell(this Excel.Worksheet workSheet, int row, string col, object o, string numberFormat=null)
        {
            int iCol = GetCol(col);
            workSheet.SetCell(row, iCol, o, numberFormat);
        }
        public static void SetCell(this Excel.Worksheet workSheet, string cellName, object o, string numberFormat = null)
        {
            RowCol<int, int> rc = GetRowCol(cellName);
            workSheet.SetCell(rc.row, rc.col, o, numberFormat);
        }

        public static object GetCell(this Excel.Worksheet workSheet, int row, string col)
        {
            int iCol = GetCol(col);
            return workSheet.Cells[row, iCol];
        }
        public static object GetCell(this Excel.Worksheet workSheet, int row, int col)
        {
            return (workSheet.Cells[row, col] as Excel.Range).Value;
        }

        public static object GetCell(this Excel.Worksheet workSheet, string cellName)
        {
            RowCol<int, int> rc = GetRowCol(cellName);
            return GetCell(workSheet, rc.row, rc.col);
        }
    }
}

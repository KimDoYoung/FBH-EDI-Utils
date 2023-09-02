using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdiUtils.Common
{
    public static class TableExtension
    {
        /// <summary>
        /// 셀명 A10, B11과 같은 엑셀 셀명으로 테이블의 데이터를 가져온다.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="cellName">B4와 같은 형태</param>
        public static string CellAsString(this DataTable table, string cellName)
        {
            RowCol<int, int> rc = GetRowColFromCellName(cellName);

            object v = table.Rows[rc.row][rc.col];
            if (v == null || string.IsNullOrEmpty(v.ToString())) {
                return "";
            }
            return v.ToString();
        }
        public static int? CellAsInteger(this DataTable table, string cellName)
        {
            RowCol<int, int> rc = GetRowColFromCellName(cellName);

            object v = table.Rows[rc.row][rc.col];
            if (v == null || string.IsNullOrEmpty(v.ToString()))
            {
                return null;
            }
            return Convert.ToInt32(v.ToString());
        }
        public static decimal? CellAsDecimal(this DataTable table, string cellName)
        {
            RowCol<int, int> rc = GetRowColFromCellName(cellName);

            object v = table.Rows[rc.row][rc.col];
            if (v == null || string.IsNullOrEmpty(v.ToString()))
            {
                return null;
            }
            return Convert.ToDecimal(v.ToString());
        }

        private static RowCol<int, int> GetRowColFromCellName(string cellName)
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
                int indexRow = Convert.ToInt32(row) - 1;
                int indexCol = ConvertExcelColNameToIndex(col);
                CommonUtil.Console(indexRow + ", " + indexCol);
                return new RowCol<int, int>(indexRow, indexCol);
            }
            return null;

        }
        private static int ConvertExcelColNameToIndex(string col)
        {
            if (col.Length == 1)
            {
                return Convert.ToChar(col) - 'A';
            }
            else if (col.Length == 2)
            {
                //AA index => 26+ 0 
                return ((Convert.ToChar(col[0]) - 'A' + 1) * 26) + (Convert.ToChar(col[1]) - 'A') + 1;
            }
            else
            {
                throw new EdiUtilsException("엑셀 셀문자열을 column index로 변환하는데 실패했습니다");
            }
        }
    }
}

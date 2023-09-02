using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiUtils.Common
{
    public static class DataRowExtension
    {
        public static string CellAsString(this DataRow row, string cellName)
        {
            int colIndex = GetIndex(cellName);
            if (row[colIndex] != null && string.IsNullOrEmpty(row[colIndex].ToString()) == false)
            {
                return row[colIndex].ToString();
            }
            return "";
        }
        public static int? CellAsInteger(this DataRow row, string cellName)
        {
            int colIndex = GetIndex(cellName);
            if (row[colIndex] != null && string.IsNullOrEmpty(row[colIndex].ToString()) == false)
            {
                return Convert.ToInt32(row[colIndex].ToString());
            }
            return null;
        }
        public static decimal? CellAsDecimal(this DataRow row, string cellName)
        {
            int colIndex = GetIndex(cellName);
            if (row[colIndex] != null && string.IsNullOrEmpty(row[colIndex].ToString()) == false)
            {
                return Convert.ToDecimal(row[colIndex].ToString());
            }
            return null;
        }


        private static int GetIndex(string cellName)
        {
            int colIndex;
            if (cellName.Length == 1)
            {
                colIndex = Convert.ToChar(cellName) - 'A';
            }
            else if (cellName.Length == 2)
            {
                colIndex = ((Convert.ToChar(cellName[0]) - 'A' + 1) * 26) + (Convert.ToChar(cellName[1]) - 'A');
            }
            else
            {
                throw new Exception("{cellName} 은 index로 환산될 수 없습니다.");
            }

            return colIndex;
        }
    }
}

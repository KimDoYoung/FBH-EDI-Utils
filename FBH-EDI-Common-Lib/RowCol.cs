using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public class RowCol<T, U>
    {
        public T row { get; set; }
        public U col { get; set; }

        public RowCol(T row, U col)
        {
            this.row = row;
            this.col = col;
        }

        public override string ToString()
        {
            return $"({row}, {col})";
        }
    };
}

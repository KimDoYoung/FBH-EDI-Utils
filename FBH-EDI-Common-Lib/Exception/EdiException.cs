using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public class EdiException : Exception
    {
        public EdiException()
        {
        }

        public EdiException(string message)
            : base(message)
        {
        }

        public EdiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

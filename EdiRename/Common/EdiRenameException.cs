using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EdiRename.Common
{
    [Serializable]
    internal class EdiRenameException : Exception
    {
        public EdiRenameException()
        {
        }

        public EdiRenameException(string message) : base(message)
        {
        }

        public EdiRenameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EdiRenameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

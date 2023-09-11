using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public interface IConfig
    {
        void Save(string uri);
        void Save();
        void Load(string uri);
        String Get(string key);
        String Get(string key, string defaultValue);
        void Set(string key, string value);
    }
}

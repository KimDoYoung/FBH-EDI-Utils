using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public class FileConfig : IConfig
    {
        public Dictionary<String, String> map = new Dictionary<string, string> ();
        
        public string Get(string key)
        {
            if( map.ContainsKey(key) )
            {
                return map[key];
            }
            return "";
        }
        public void Set(string key, string value)
        {
            map[key] = value;
        }


        public void Load(string uri)
        {
            if (File.Exists(uri) == false) return;
            string s = File.ReadAllText(uri);
            string[] lines = s.Split('\n');
            map.Clear();
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("#")) continue; //# 로 시작되면 스킵
                if (line.Trim().Length < 1) continue; //빈라인 스킵

                string[] items = line.Split('|');
                string key = items[0].Trim();
                string value = items[1].Trim(); 
                map[key] = value;   
            }
        }

        public void Save(string uri)
        {
            StringBuilder sb = new StringBuilder(); 
            foreach (var item in map)
            {

                sb.AppendLine($"{item.Key}|{item.Value}");
            }
            
            File.WriteAllText(uri, sb.ToString());
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FBH.EDI.Common
{
    public class FileConfig : IConfig
    {
        public Dictionary<String, String> map;// = new Dictionary<string, string> ();
        private String filePath;
        public FileConfig(String filePath)
        {
            this.filePath = filePath; 
            map = new Dictionary<String, String> ();
            Load(this.filePath);
        }
        public string Get(string key)
        {
            if( map.ContainsKey(key) )
            {
                return map[key];
            }
            return "";
        }
        public string Get(string key, string defaultValue)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }
            Set(key, defaultValue);
            
            return defaultValue;
        }

        public void Set(string key, string value)
        {
            map[key] = value;
        }
        public void Load()
        {
            Load(this.filePath);
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
        public void Save()
        {
            this.Save(this.filePath);
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

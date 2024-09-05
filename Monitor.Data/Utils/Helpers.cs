using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace ACS.Monitor.Utilities
{
    public  class Helpers
    {
        public static string ConvertDictionaryToString<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            try
            {
                var result = JsonConvert.SerializeObject(dict);
                return result;
            }
            catch { }
            return null;

            //string format = "{0}=`{1}`|";
            //var sb = new StringBuilder();
            //foreach (var kv in dict)
            //{
            //    sb.AppendFormat(format, kv.Key, kv.Value);
            //}
            //if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);

            //return sb.ToString();
        }


        public static Dictionary<string, string> ConvertStringToDictionary(string dictString)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictString);
                return result;
            }
            catch { }
            return null;

            //var result = new Dictionary<string, string>();
            //var items = dictString.Split('|').Select(x => x.Trim().Split('='));
            //foreach (string[] tokens in items)
            //{
            //    if (tokens.Length == 2)
            //    {
            //        string k = tokens[0];
            //        string v = tokens[1].Trim('`');

            //        if (result.ContainsKey(k) == false)
            //            result.Add(k, v);
            //    }
            //}
            //return result;
        }

        public static Dictionary<int, string> ConvertintToDictionary(string dictString)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<Dictionary<int, string>>(dictString);
                return result;
            }
            catch { }

            return null;
        }

        // form class를 만날때 까지 계속 부모 개체를 찾아 올라가는 함수
        public static Form GetParentForm(Control control)
        {
            if (control.Parent == null) return null;
            if (control.Parent is Form)
            {
                return control.Parent as Form;
            }
            else
            {
                return GetParentForm(control.Parent);
            }
        }


        public static string GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string localIP = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = host.AddressList[i].ToString();
                    break;
                }
            }
            return localIP;
        }

    }
}

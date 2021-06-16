using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DumpHelper
{
    class Program
    {
        static string path = string.Empty;
        static string[] list;
        static List<string> result = new List<string>();
        static void Main(string[] args)
        {
            Console.Title = "DumpHelper helper by Aiko";

            if (args.Length != 0)
            {
                foreach (string word in args)
                {
                    path += word;
                }
                list = File.ReadAllLines(path);

                Console.WriteLine("Select functions:" +
                    "\n\t1. Generate dorks with keywords" +
                    "\n\t2. Dorks combiner");

                string point = Console.ReadLine();
                switch (point)
                {
                    case "1":
                        _generateWithKeywords();
                        break;
                    case "2":
                        _dorkCombiner();
                        break;
                    default:
                        Console.WriteLine("Not see this point");
                        break;
                }
            }
            else { Console.WriteLine("null"); Console.ReadKey(); }
        }


        static void _dorkCombiner()
        {
            List<string> page = new List<string>();
            List<string> param = new List<string>();
            string[] type = {".php", "html", "" };
            try
            {
                foreach (string str in list)
                {
                    try
                    {
                        string r = str.Split('/')[str.ToCharArray().Where(w => w == "/"[0]).Count()];
                        if (r.Contains("."))
                        {
                            page.Add(r.Remove(r.LastIndexOf('.'), r.Length - r.LastIndexOf('.')));
                        }
                        else if (r.Contains("?"))
                        {
                            param.Add(string.Concat("?", Pars(r, "?", "="), "="));
                            page.Add(r.Remove(r.LastIndexOf('?'), r.Length - r.LastIndexOf('?')));
                        }
                    }
                    catch { }
                }

                foreach (string a in page)
                {
                    foreach (string b in type)
                    {
                        foreach (string c in param)
                        {
                            result.Add(string.Concat(a, b, c));
                        }
                    }
                }
                File.WriteAllLines(Path.Combine(Application.StartupPath.ToString(), "Combined " + DateTime.UtcNow.ToString().Replace(":", "-") + " Log.txt"), result);
            }
            catch { }
        }
        static void _generateWithKeywords()
        {
            try
            {
                foreach (string str in list)
                {
                    result.Add(string.Concat(str, " php?"));
                }
                File.WriteAllLines(Path.Combine(Application.StartupPath.ToString(), "KeyWords " + DateTime.UtcNow.ToString().Replace(":", "-") + " Log.txt"), result);
            }
            catch { }
        }

        public static string Pars(string strSource, string strStart, string strEnd, int startPos = 0) //method for parsing answer
        {
            string result = string.Empty;
            try
            {
                int length = strStart.Length, num = strSource.IndexOf(strStart, startPos), num2 = strSource.IndexOf(strEnd, num + length);
                if (num != -1 & num2 != -1)
                {
                    result = strSource.Substring(num + length, num2 - (num + length));
                }
            }
            catch {}
            return result;
        }
    }
}

using System;
using System.IO;
using System.Net;

namespace rating_parse
{
    class Program
    {
        static private string get_rating(string Name, string Surname)
        {
            string urlStr = "http://www.shogi.net/fesa/index.php?mid=5&player=" + Name + "+" + Surname;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlStr);
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            response.Close();



            int first_kyu = s.IndexOf("Kyu</td><td>");
            int first_dan = s.IndexOf("Dan</td><td>");

            if (Math.Abs(first_kyu - first_dan) == 14)
            {
                int n = Math.Max(first_dan, first_kyu);
                n += 12;
                string ss = s.Substring(n, s.IndexOf("<", n));
                return ss;
            }

            int second_kyu = 0;
            int second_dan = 0;

            //if ((first_kyu != -1) && (first_dan != -1))
            //{
            //    second_kyu = s.IndexOf("Kyu", first_kyu + 3);
            //    second_dan = s.IndexOf("Dan", first_dan + 3);

            //    if (second_kyu - first_kyu < 20) return s.Substring(second_kyu + 12, s.IndexOf("<", second_kyu + 12) - second_kyu - 12);
            //    if (second_dan - first_dan < 20) return s.Substring(second_dan + 12, s.IndexOf("<", second_dan + 12) - second_dan - 12);

            //    if (Math.Abs(first_kyu - first_dan) < 20)
            //    {
            //        int temp = Math.Max(first_dan, first_kyu);
            //        return s.Substring(temp + 12, s.IndexOf("<", temp + 12) - temp - 12);
            //    }

            //}
            //if (first_kyu != -1) return s.Substring(first_kyu + 12, s.IndexOf("<", first_kyu + 12) - first_kyu - 12);
            //if (first_dan != -1) return s.Substring(first_dan + 12, s.IndexOf("<", first_dan + 12) - first_dan - 12);

            //if ((first_kyu == -1) && (first_dan == -1)) return "1";
            ////Math.Max()

            ////int followers = Int32.Parse(s.Substring(beg, s.IndexOf(",", beg) - beg));
            ////beg = s.IndexOf("views_count") + 13;
            ////int views = Int32.Parse(s.Substring(beg, s.IndexOf(",", beg) - beg));
            ////Console.WriteLine("{0,-18} {1,6} {2,8}", "ass", followers, views);

            return "";
        }

        static void Main(string[] args)
        {
            String[] name = { "Hideaki" ,  "Toshihiko" };
            String[] surname = { "Takahashi", "Otsuka" };

            for (int i = 0; i < name.Length; i++)
            {
                Console.WriteLine("{0,-18} {1,6} {2,8}", surname[i], name[i], get_rating(name[i],surname[i]));
            }
            Console.ReadKey();
        }
    }
}

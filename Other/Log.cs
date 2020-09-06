using System.IO;
using System;

namespace erecruiter
{
    public class Log 
    {
        public static void Add(string message)
        {
            string time = DateTime.Now.ToString();
            File.AppendAllText("log", time + " " + message + Environment.NewLine);
        }
    }
}

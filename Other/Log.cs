using System.IO;

namespace erecruiter
{
    public class Log 
    {
        public static void Add(string message)
        {
            File.WriteAllText("log", message);
        }
    }
}

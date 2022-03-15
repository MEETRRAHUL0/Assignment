using System;
using System.IO;

namespace Assignment
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Assignment\n");

            bool exit;
            do
            {
                var res = FileHelper.GetFiles();
                exit = res.Item2;

                if (res.Item2 || res.Item1 == null) continue;

                bool exitOptions;
                do
                {
                    exitOptions = FileHelper.Options(res.Item1);
                } while (!exitOptions);

            } while (!exit);
        }
    }
}
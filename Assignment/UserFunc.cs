using System;
using System.IO;


namespace Assignment
{
    public class UserFunc
    {
        public static readonly Func<string, string> ReadValue = (string value) =>
        {
            Console.WriteLine($"{value}\n");
            return Console.ReadLine();
        };


        public static Func<string, string, bool> Compare = (string f1, string f2) =>
        {
            byte[] file1 = File.ReadAllBytes(f1);
            byte[] file2 = File.ReadAllBytes(f2);

            if (file1.Length != file2.Length) return false;

            for (int i = 0; i < file1.Length; i++)
            {
                if (file1[i] != file2[i])
                {
                    return false;
                }
            }
            return true;

        };
    }
}

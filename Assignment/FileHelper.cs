using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Assignment
{
    public class FileHelper
    {
        public static bool Options(FileInfo files)
        {
            Console.WriteLine(@"
    1. Word occurrence.
    2. Character occurrence
    3. Lines Count
    4. Word Count
    5. Char Count

    6. Most Frequently & longest word
    7. Text in reverse order
    8. comparison files

    9: Exit

            ");

            var fileOption = UserFunc.ReadValue("Select Options:").ToLower();


            if (fileOption == "9" || fileOption == "exit") return true;

            if (!int.TryParse(fileOption, out int SelectedOption))
            {
                Console.WriteLine("Only Integer Value \n");
                return false;
            }

            var txtfile = File.ReadAllText(files.FullName);

            if (SelectedOption == 1)
            {
                string word = UserFunc.ReadValue("word to find.");
                Console.WriteLine($"Count : {txtfile.Split(' ').Where(w => w.ToLower() == word.ToLower()).Count()}\n");
            }
            else if (SelectedOption == 2)
            {
                string @char = UserFunc.ReadValue("char to find.");

                if (char.TryParse(@char, out char chartoFind))
                    Console.WriteLine($"Count : {txtfile.ToCharArray().Count(c => c == chartoFind)}\n");
                else
                    Console.WriteLine("Invalid char.\n");
            }
            else if (SelectedOption == 3) { Console.WriteLine($"Count : {txtfile.Split('\r').Length}\n"); }
            else if (SelectedOption == 4) { Console.WriteLine($"Count : {txtfile.Split(' ').Count()}\n"); }
            else if (SelectedOption == 5) { Console.WriteLine($"Count : {txtfile.ToArray().Where(c => !char.IsWhiteSpace(c)).Select(s => s).Count()}\nChar Count with Space: {txtfile.ToCharArray().Count()}\n"); }
            else if (SelectedOption == 6)
            {
                var counts = Regex.Replace(txtfile, @"[^0-9a-zA-Z ]+", "").Split(' ')
                             .GroupBy(g => g)
                             .OrderByDescending(g => g.Count())
                             .Select(w => new { w.Key, count = w.Count(), Length = w.Key.Length })
                             .Where(w => w.count > 1).Take(5).ToList();

                var count = 1;

                Console.WriteLine($"Word and its frequency:\n");
                counts.ForEach(w => Console.WriteLine($"{count++}: {w.Key} => {w.count} Times"));

                var longestWord = counts?.OrderByDescending(w => w.Length)?.FirstOrDefault();
                Console.WriteLine($"\nlongest word is [{longestWord?.Key}] with Length {longestWord?.Length}\n");
            }
            else if (SelectedOption == 7)
            {

                var wordArr = txtfile.Split(' ');

                Array.Reverse(wordArr);

                Console.WriteLine($"Original Text :\n {txtfile}\n");
                Console.WriteLine($"\n===================================================================\n");
                Console.WriteLine($"Reverse Text : \n{string.Join(" ", wordArr)}\n");
            }
            else if (SelectedOption == 8)
            {
                var allFiles = FileHelper.ShowFiles();

                string fileTwoID = UserFunc.ReadValue("Select 2nd File to Compare");

                if (!int.TryParse(fileTwoID, out int fileIndex))
                {
                    Console.WriteLine("Invalid Selection\n");
                    return false;
                }

                if (fileIndex < 1 || fileIndex > 4)
                {
                    Console.WriteLine("Invalid Selection\n");
                    return false;
                }

                var fileInfoOne = allFiles.Where(w => w.FullName == files.FullName).FirstOrDefault();
                var fileInfoTwo = allFiles[fileIndex - 1];
                Console.WriteLine($"Selected File: {fileInfoTwo.Name}\n");

                Console.WriteLine(UserFunc.Compare(fileInfoOne.FullName, fileInfoTwo.FullName) ? "Files are Same" : "Files are Not Same\n");
            }

            return false;
        }

        public static (FileInfo, bool) GetFiles()
        {
            var files = ShowFiles();

            string fileId = UserFunc.ReadValue("Select File").ToLower();

            if (fileId == "5" || fileId == "exit")
            {
                return (null, true);
            }

            if (!int.TryParse(fileId, out int fileIndex))
            {
                Console.WriteLine("Integer value Only!\n");
                return (null, false);
            }

            if (fileIndex < 1 || fileIndex > 4)
            {
                Console.WriteLine("Invalid Selection\n");
                return (null, false); ;
            }

            var fileInfo = files[fileIndex - 1];

            return (fileInfo, false);
        }

        public static List<FileInfo> ShowFiles()
        {
            var fileInfo = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}ExampleFiles", "*.txt", SearchOption.TopDirectoryOnly)
                        .Select((file, index) => (new FileInfo(file), index))
                        .OrderBy(w => w.Item1.Name)
                        .Take(4)
                        .Select(s => { Console.WriteLine($"{s.index + 1} : {s.Item1.Name}"); return s.Item1; })
                        .ToList();

            Console.WriteLine($"5 : Exit");

            return fileInfo;
        }
    }
}
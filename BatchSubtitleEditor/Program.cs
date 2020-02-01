using System;
using System.IO;

namespace BatchSubtitleEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------Welcome To Batch Subtitle Editor-------");
            Console.WriteLine("\n" +
                "Enter the path to the folder containing your subtitles." +
                "\n" +
                "The program will automatically search for .srt files.");
            var path = Console.ReadLine();

            var files = Directory.GetFiles(path, "*.srt");
            foreach (var file in files)
            {
                
            }

            
        }
    }
}

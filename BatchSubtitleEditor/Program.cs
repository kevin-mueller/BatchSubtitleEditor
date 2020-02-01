using SubtitlesParser.Classes;
using SubtitlesParser.Classes.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                "The program will automatically search for .srt files. (Also scanning for subdirectories)");
            var path = Console.ReadLine();

            Console.WriteLine("Enter the timeshift you want to apply: (in milliseconds) ");
            var timeshiftString = Console.ReadLine();
            int timeShift = 0;
            try
            {
                timeShift = Convert.ToInt32(timeshiftString);
            } catch
            {
                Console.WriteLine("Please enter a number.");
                Console.ReadKey();
                Environment.Exit(1);
            }

            var files = Directory.GetFiles(path, "*.srt", SearchOption.AllDirectories);
            foreach (var filePath in files)
            {
                var parser = new SubParser();
                List<SubtitleItem> itemsShifted = null;
                List<SubtitleItem> itemsOriginal = new List<SubtitleItem>();
                using (var fileStream = File.OpenRead(filePath))
                {
                    try
                    {
                        itemsShifted = parser.ParseStream(fileStream);

                        //Copy to original list without copying the reference
                        foreach (var _ in itemsShifted)
                        {
                            itemsOriginal.Add(new SubtitleItem()
                            {
                                EndTime = _.EndTime,
                                StartTime = _.StartTime,
                                Lines = _.Lines
                            });
                        }


                        Console.WriteLine($"Shifting Subtitle: {Path.GetFileName(filePath)} by {timeShift} milliseconds.");
                        foreach (var item in itemsShifted)
                        {
                            item.StartTime += timeShift;
                            item.EndTime += timeShift;
                        }
                        
                    }
                    catch
                    {
                        Console.WriteLine($"Not a valid subtitle format: {filePath}");
                    }
                }
                parser = null;
                
                var res = SubtitleEditor.SaveItemsToFile(itemsShifted, itemsOriginal, filePath);
                if (!res)
                    Console.WriteLine("Error writing to subtitle.");
                else Console.WriteLine("Subtitle successfully edited!");

            }

            
        }
    }
}

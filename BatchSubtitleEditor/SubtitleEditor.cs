using SubtitlesParser.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BatchSubtitleEditor
{
    public static class SubtitleEditor
    {
        public static bool SaveItemsToFile(List<SubtitleItem> itemsShifted, List<SubtitleItem> itemsOriginal, string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            var originalFileContent = fileContent;

            for (int i = 0; i < itemsShifted.Count; i++)
            {
                var originalItem = itemsOriginal[i];

                var searchString = ConvertItemToValidString(originalItem);

                fileContent = fileContent.Replace(searchString, ConvertItemToValidString(itemsShifted[i]));
            }

            File.WriteAllText(filePath, fileContent);

            return !fileContent.Equals(originalFileContent);
        }

        private static string ConvertItemToValidString(SubtitleItem item)
        {
            var startTime = TimeSpan.FromMilliseconds(item.StartTime).ToString().Replace(".", ",");
            startTime = startTime[0..^4];
            var endTime = TimeSpan.FromMilliseconds(item.EndTime).ToString().Replace(".", ",");
            endTime = endTime[0..^4];
            return startTime + " --> " + endTime;
        }
    }
}

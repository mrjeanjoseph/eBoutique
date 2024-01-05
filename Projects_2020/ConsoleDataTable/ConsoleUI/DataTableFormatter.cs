using System;
using System.Linq;

namespace ConsoleUI
{
    public static class DataTableFormatter
    {
        private const int tableWidth = 115;
        public static void LineSeparator()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        public static void PrintRow(params string[] columns)
        {
            int columnWidth = (tableWidth - columns.Length) / columns.Length;
            const string emptySpaces = " ";

            string row = columns.Aggregate(emptySpaces, (separator, columnText) => separator + GetCenterAllignedText(columnText, columnWidth) + emptySpaces);
            Console.WriteLine(row);
        }

        private static string GetCenterAllignedText(string columnText, int columnWidth)
        {
            columnText = columnText.Length > columnWidth ? columnText.Substring(0, columnWidth - 1) + "..." : columnText;

            return string.IsNullOrEmpty(columnText)
                ? new string(' ', columnWidth)
                : columnText.PadRight(columnWidth);
        }
    }

}

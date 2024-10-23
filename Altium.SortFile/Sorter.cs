namespace Altium.SortFile
{
    public static class Sorter
    {
        public static string SortAndSaveChunk(List<(string line, string textPart, int number)> lines, int chunkIndex, string tempDirectory)
        {
            lines.Sort((a, b) =>
            {
                string textA = a.line.Substring(a.line.IndexOf('.') + 2);
                string textB = b.line.Substring(b.line.IndexOf('.') + 2);
                int textComparison = string.Compare(textA, textB, StringComparison.Ordinal);

                if (textComparison == 0)
                {
                    return a.number.CompareTo(b.number);
                }

                return textComparison;
            });

            string chunkFile = $"{tempDirectory}/chunk_{chunkIndex}.txt";
            File.WriteAllLines(chunkFile, lines.Select(l => l.line));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"- New chunk was created: {chunkFile}");
            return chunkFile;
        }
    }
}

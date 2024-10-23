namespace Altium.SortFile
{
    public static class Extractor
    {
        public static (string textPart, int number) ExtractParts(string line)
        {
            int startIndex = 0;
            while (startIndex < line.Length && !char.IsDigit(line[startIndex]))
            {
                startIndex++;
            }
            string textPart = line.Substring(0, startIndex);

            int numberStartIndex = startIndex;
            int number = ExtractNumber(line, ref numberStartIndex);

            return (textPart, number);
        }

        public static int ExtractNumber(string line, ref int startIndex)
        {
            try
            {
                int num = 0;
                while (startIndex < line.Length && char.IsDigit(line[startIndex]))
                {
                    num = num * 10 + (line[startIndex] - '0');
                    startIndex++;
                }
                return num;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while extracting number from line: {ex.Message}");
                throw;
            }
        }
    }
}

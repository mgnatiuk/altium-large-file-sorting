using System.Diagnostics;
using System.Text;

class Program
{
    static long targetFileSizeInGb = 1;

    static string outputFilePath = $"../Shared/input_{targetFileSizeInGb}gb.txt";

    const int BatchSize = 1_000_00;
    static long totalLinesProcessed = 0;

    static void Main(string[] args)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        using (var fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        using (var bufferedStream = new BufferedStream(fileStream))
        using (var writer = new StreamWriter(bufferedStream))
        {
            var batch = new List<string>(BatchSize);
            long currentSize = 0;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--- STARTING FILE GENERATION ---");
            Console.ForegroundColor = ConsoleColor.Yellow;

            while (currentSize < targetFileSizeInGb * 1024 * 1024 * 1024)
            {
                string line = GenerateRandomLine(++totalLinesProcessed);
                batch.Add(line);
                currentSize += Encoding.UTF8.GetByteCount(line) + Environment.NewLine.Length;

                if (batch.Count >= BatchSize)
                {
                    writer.WriteLine(string.Join(Environment.NewLine, batch));
                    batch.Clear();
                }
            }
        }

        stopwatch.Stop();

        FileInfo fileInfo = new FileInfo(outputFilePath);
        Console.WriteLine($"\n--- FILE GENERATION HAS BEEN FINISHED ---");
        Console.WriteLine($"Total lines generated: {totalLinesProcessed:N0}");
        Console.WriteLine($"File size: {fileInfo.Length / (1024 * 1024)} MB.");
        Console.WriteLine($"Execution time: {stopwatch.Elapsed:mm\\:ss}"); // Виводимо, скільки часу зайняло
    }

    // Generate a string in the format "number. text"
    static string GenerateRandomLine(long index)
    {
        long randomNumber = Random.Shared.Next(1, 1000000);
        string randomText = GenerateRandomText(Random.Shared.Next(5, 50));
        return $"{randomNumber}. {randomText}";
    }

    // Generate random text of a specified length
    static string GenerateRandomText(long length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var stringBuilder = new StringBuilder((int)length);

        int charLength = chars.Length;

        for (long i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[Random.Shared.Next(charLength)]);
        }

        return stringBuilder.ToString();
    }
}
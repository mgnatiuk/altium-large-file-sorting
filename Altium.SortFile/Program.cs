using System.Diagnostics;

namespace Altium.SortFile;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            string inputFilePath = $"../Shared/input_1gb.txt";
            string outputFilePath = "../Shared/sorted_output.txt";
            int chunkSize = 2_000_000;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n--- STARTING PROCESSING FILE: {inputFilePath.ToUpper()} ---");
            SortLargeFile(inputFilePath, outputFilePath, chunkSize);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void SortLargeFile(string inputFilePath, string outputFilePath, int chunkSize)
    {
        List<string> chunkFiles = new List<string>();
        Directory.CreateDirectory("../Shared/Temp");

        Stopwatch stopwatch = Stopwatch.StartNew();
        int totalLinesProcessed = 0;

        try
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                List<string> lines = new List<string>(chunkSize);
                string line;
                int chunkIndex = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                    totalLinesProcessed++;

                    if (lines.Count >= chunkSize)
                    {
                        chunkFiles.Add(SortAndSaveChunk(lines, chunkIndex++));
                        lines.Clear();
                    }
                }

                if (lines.Count > 0)
                {
                    chunkFiles.Add(SortAndSaveChunk(lines, chunkIndex));
                }
            }

            MergeChunks(chunkFiles, outputFilePath);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while processing the file: {ex.Message}");
        }
        finally
        {
            stopwatch.Stop();
            CleanupTempFiles(chunkFiles);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n--- PROCESSING HAS BEEN FINISHED ---");
            Console.WriteLine($"Total chunks created: {chunkFiles.Count}");
            Console.WriteLine($"Total lines processed: {totalLinesProcessed:N0}");
            Console.WriteLine($"Execution time: {stopwatch.Elapsed:mm\\:ss}");
            Console.WriteLine($"Output file: {outputFilePath}");
        }
    }

    public static string SortAndSaveChunk(List<string> lines, int chunkIndex)
    {
        try
        {
            SortLines(lines);
            string chunkFile = $"../Shared/Temp/chunk_{chunkIndex}.txt";
            File.WriteAllLines(chunkFile, lines);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"- New chunk was created: {chunkFile}");
            return chunkFile;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while sorting and saving chunk: {ex.Message}");
            throw;
        }
    }

    public static void SortLines(List<string> lines)
    {
        try
        {
            lines.Sort(CompareLines);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while sorting lines: {ex.Message}");
            throw;
        }
    }

    public static void MergeChunks(List<string> chunkFiles, string outputFilePath)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n--- MERGING CHUNKS HAS BEEN STARTED ---");

        List<StreamReader> readers = new List<StreamReader>();

        try
        {
            foreach (var chunkFile in chunkFiles)
            {
                readers.Add(new StreamReader(chunkFile));
            }

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                PriorityQueue<(string line, int index), string> priorityQueue = new PriorityQueue<(string, int), string>();

                for (int i = 0; i < readers.Count; i++)
                {
                    if (!readers[i].EndOfStream)
                    {
                        string line = readers[i].ReadLine();
                        priorityQueue.Enqueue((line, i), line);
                    }
                }

                while (priorityQueue.Count > 0)
                {
                    var smallest = priorityQueue.Dequeue();
                    writer.WriteLine(smallest.line);

                    int readerIndex = smallest.index;
                    if (!readers[readerIndex].EndOfStream)
                    {
                        string nextLine = readers[readerIndex].ReadLine();
                        priorityQueue.Enqueue((nextLine, readerIndex), nextLine);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred during merging: {ex.Message}");
            throw;
        }
        finally
        {
            foreach (var reader in readers)
            {
                reader.Dispose();
            }
        }
    }

    public static int CompareLines(string line1, string line2)
    {
        try
        {
            int index1 = 0, index2 = 0;
            int num1 = ExtractNumber(line1, ref index1);
            int num2 = ExtractNumber(line2, ref index2);

            int textComparison = String.CompareOrdinal(line1, index1, line2, index2, Math.Min(line1.Length - index1, line2.Length - index2));

            return textComparison != 0 ? textComparison : num1.CompareTo(num2);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred while comparing lines: {ex.Message}");
            throw;
        }
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

    private static void CleanupTempFiles(List<string> chunkFiles)
    {
        foreach (var chunkFile in chunkFiles)
        {
            try
            {
                File.Delete(chunkFile);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error deleting chunk file {chunkFile}: {ex.Message}");
            }
        }

        try
        {
            Directory.Delete("../Shared/Temp");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error deleting temporary directory: {ex.Message}");
        }
    }
}

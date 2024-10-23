using System.Diagnostics;

namespace Altium.SortFile
{
    public class FileSorter
    {
        private string tempDirectory = "../Shared/Temp";

        public void SortLargeFile(string inputFilePath, string outputFilePath, int chunkSize)
        {
            List<string> chunkFiles = new List<string>();
            Directory.CreateDirectory(tempDirectory);

            Stopwatch stopwatch = Stopwatch.StartNew();
            int totalLinesProcessed = 0;

            try
            {
                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    List<(string line, string textPart, int number)> lines = new List<(string line, string textPart, int number)>(chunkSize);
                    string line;
                    int chunkIndex = 1;

                    while ((line = reader.ReadLine()) != null)
                    {
                        (string textPart, int number) = Extractor.ExtractParts(line);
                        lines.Add((line, textPart, number));
                        totalLinesProcessed++;

                        if (lines.Count >= chunkSize)
                        {
                            chunkFiles.Add(Sorter.SortAndSaveChunk(lines, chunkIndex++, tempDirectory));
                            lines.Clear();
                        }
                    }

                    if (lines.Count > 0)
                    {
                        chunkFiles.Add(Sorter.SortAndSaveChunk(lines, chunkIndex, tempDirectory));
                    }
                }

                Merger.MergeChunks(chunkFiles, outputFilePath);
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

        public void CleanupTempFiles(List<string> chunkFiles)
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
                Directory.Delete(tempDirectory);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error deleting temporary directory: {ex.Message}");
            }
        }
    }
}

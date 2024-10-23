namespace Altium.SortFile
{
    public static class Merger
    {
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
                    PriorityQueue<(string line, string textPart, int number, int index), (string textPart, int number)> priorityQueue = new PriorityQueue<(string, string, int, int), (string, int)>();

                    for (int i = 0; i < readers.Count; i++)
                    {
                        if (!readers[i].EndOfStream)
                        {
                            string line = readers[i].ReadLine();
                            (string textPart, int number) = Extractor.ExtractParts(line);
                            priorityQueue.Enqueue((line, textPart, number, i), (textPart, number));
                        }
                    }

                    while (priorityQueue.Count > 0)
                    {
                        var smallest = priorityQueue.Dequeue();
                        writer.WriteLine(smallest.line);

                        var reader = readers[smallest.index];
                        if (!reader.EndOfStream)
                        {
                            string nextLine = reader.ReadLine();
                            (string textPart, int number) = Extractor.ExtractParts(nextLine);
                            priorityQueue.Enqueue((nextLine, textPart, number, smallest.index), (textPart, number));
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
    }
}

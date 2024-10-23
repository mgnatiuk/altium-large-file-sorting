namespace Altium.SortFile
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputFilePath = $"../Shared/test.txt";
                string outputFilePath = "../Shared/sorted_output.txt";
                int chunkSize = 2_000_000;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n--- STARTING PROCESSING FILE: {inputFilePath.ToUpper()} ---");
                FileSorter fileSorter = new FileSorter();
                fileSorter.SortLargeFile(inputFilePath, outputFilePath, chunkSize);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

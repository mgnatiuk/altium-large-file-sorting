using System.Diagnostics;
using System.Text;

class Program
{
    static long targetFileSizeInGb = 1;
    static string outputFilePath = $"../Shared/input_{targetFileSizeInGb}gb.txt";

    const int BatchSize = 1_000_00;
    static long totalLinesProcessed = 0;

    // Predefined list of words
    static readonly List<string> WordList = new List<string>
    {
        "apple", "banana", "orange", "grape", "kiwi",
        "mango", "peach", "pear", "cherry", "plum",
        "strawberry", "blueberry", "raspberry", "pineapple", "watermelon",
        "coconut", "papaya", "apricot", "lemon", "lime",
        "pomegranate", "tangerine", "fig", "date", "cantaloupe",
        "blackberry", "dragonfruit", "passionfruit", "nectarine", "clementine",
        "persimmon", "blood orange", "jackfruit", "custard apple", "starfruit",
        
        // Vegetables
        "carrot", "broccoli", "spinach", "cauliflower", "potato",
        "tomato", "cucumber", "pepper", "eggplant", "zucchini",
        "asparagus", "kale", "beet", "lettuce", "onion",
        
        // Animals
        "dog", "cat", "elephant", "tiger", "lion",
        "zebra", "giraffe", "kangaroo", "panda", "bear",
        "dolphin", "whale", "eagle", "shark", "penguin",
        
        // Miscellaneous
        "computer", "phone", "table", "chair", "book",
        "car", "bicycle", "train", "airplane", "rocket",
        "guitar", "piano", "drum", "violin", "flute",
        
        // Adjectives
        "happy", "sad", "bright", "dark", "colorful",
        "beautiful", "ugly", "quick", "slow", "smooth",
        "rough", "soft", "hard", "warm", "cold",
        
        // Places
        "beach", "mountain", "forest", "river", "city",
        "village", "desert", "island", "ocean", "lake",
        
        // Actions
        "run", "jump", "swim", "dance", "sing",
        "write", "read", "draw", "paint", "cook"
    };

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
        Console.WriteLine($"Execution time: {stopwatch.Elapsed:mm\\:ss}"); // Output execution time
    }

    // Generate a line with a random number followed by 1 to 4 random words from WordList
    static string GenerateRandomLine(long index)
    {
        int numberOfWords = Random.Shared.Next(1, 5); // Random number of words (1 to 4)
        var words = new List<string>();

        for (int i = 0; i < numberOfWords; i++)
        {
            words.Add(SelectRandomWord());
        }

        // Generate a random number for the line
        long randomNumber = Random.Shared.Next(1, 1_000_000);

        return $"{randomNumber}. {string.Join(" ", words)}"; // Prepend the number to the generated string
    }

    // Select a random word from the WordList
    static string SelectRandomWord()
    {
        return WordList[Random.Shared.Next(WordList.Count)];
    }
}

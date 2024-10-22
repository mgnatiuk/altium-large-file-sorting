using FluentAssertions;

public class SortingTests
{
    [Fact]
    public void SortLines_ShouldSortLinesCorrectly()
    {
        // Arrange
        var stub = new List<string>
            {
                "1. Apple",
                "2. Banana is yellow",
                "32. Cherry is the best",
                "415. Apple",
                "30432. Something something something"
            };

        // Act
        Altium.SortFile.Program.SortLines(stub);

        // Assert
        stub.Should()
            .BeEquivalentTo(new List<string>
                {
                    "1. Apple",
                    "415. Apple",
                    "2. Banana is yellow",
                    "32. Cherry is the best",
                    "30432. Something something something"
                });
    }

    [Fact]
    public void SortAndSaveChunk_ShouldCreateChunkFile()
    {
        // Arrange
        var stub = new List<string>
            {
                "342. Apple",
                "12453. Banana is yellow",
                "12. Orange is juicy",
                "4500. Pineapple is spiky",
                "95832. Grape is small",
                "210. Mango is sweet",
                "74. Peach is fuzzy",
                "3009. Lemon is sour",
                "432. Lime is green",
                "156789. Strawberry is red",
                "108. Watermelon is big",
                "12004. Kiwi has seeds",
                "45. Blueberry is tiny",
                "909. Raspberry is tangy",
                "11. Blackberry is dark",
                "305. Coconut is hard",
                "7391. Pomegranate has seeds",
                "612. Melon is fresh",
                "53. Papaya is exotic",
                "876. Passionfruit is aromatic",
                "1287. Guava is tropical",
                "97200. Dragonfruit is unique",
                "34. Starfruit is odd-shaped",
                "298. Avocado is creamy",
                "102. Fig is old-world",
                "6043. Date is sweet",
                "289. Persimmon is orange"
            };

        var chunkIndex = 1;
        var tempPath = "../Shared/Temp";
        Directory.CreateDirectory(tempPath);
        string expectedFilePath = Path.Combine(tempPath, $"chunk_{chunkIndex}.txt");

        // Act
        Altium.SortFile.Program.SortAndSaveChunk(stub, chunkIndex);

        // Assert
        File.Exists(expectedFilePath)
            .Should()
            .BeTrue();

        // Clean up
        File.Delete(expectedFilePath);
        Directory.Delete(tempPath);
    }

    [Fact]
    public void MergeChunks_ShouldMergeSortedChunks()
    {
        // Arrange
        var tempPath = "../Shared/Temp";
        Directory.CreateDirectory(tempPath);

        // Create two sorted chunk files
        File.WriteAllLines(Path.Combine(tempPath, "chunk_1.txt"), new[] { "a", "c" });
        File.WriteAllLines(Path.Combine(tempPath, "chunk_2.txt"), new[] { "b", "d" });

        var chunkFiles = new List<string>
            {
                Path.Combine(tempPath, "chunk_1.txt"),
                Path.Combine(tempPath, "chunk_2.txt")
            };

        string outputFilePath = Path.Combine(tempPath, "merged_output.txt");

        // Act
        Altium.SortFile.Program.MergeChunks(chunkFiles, outputFilePath);

        // Assert
        var resultLines = File.ReadAllLines(outputFilePath);

        resultLines
            .Should()
            .BeEquivalentTo(new[] { "a", "b", "c", "d" });

        // Clean up
        File.Delete(outputFilePath);
        foreach (var chunkFile in chunkFiles)
        {
            File.Delete(chunkFile);
        }
        Directory.Delete(tempPath);
    }

    [Fact]
    public void ExtractNumber_ShouldExtractNumberFromLine()
    {
        // Arrange
        var stub = new List<string>
            {
                "342. Apple",
                "12453. Banana is yellow",
                "12. Orange is juicy",
                "4500. Pineapple is spiky",
                "95832. Grape is small",
                "210. Mango is sweet",
                "74. Peach is fuzzy",
                "3009. Lemon is sour",
                "432. Lime is green",
                "156789. Strawberry is red",
                "108. Watermelon is big",
                "12004. Kiwi has seeds",
                "45. Blueberry is tiny",
                "909. Raspberry is tangy",
                "11. Blackberry is dark",
                "305. Coconut is hard",
                "7391. Pomegranate has seeds",
                "612. Melon is fresh",
                "53. Papaya is exotic",
                "876. Passionfruit is aromatic",
                "1287. Guava is tropical",
                "97200. Dragonfruit is unique",
                "34. Starfruit is odd-shaped",
                "298. Avocado is creamy",
                "102. Fig is old-world",
                "6043. Date is sweet",
                "289. Persimmon is orange"
            };

        // Act
        var extractedNumbers = new List<int>();
        foreach (var line in stub)
        {
            int startIndex = 0;
            int number = Altium.SortFile.Program.ExtractNumber(line, ref startIndex);
            extractedNumbers.Add(number);
        }

        // Assert
        extractedNumbers.Should().BeEquivalentTo(new List<int>
            {
                342,
                12453,
                12,
                4500,
                95832,
                210,
                74,
                3009,
                432,
                156789,
                108,
                12004,
                45,
                909,
                11,
                305,
                7391,
                612,
                53,
                876,
                1287,
                97200,
                34,
                298,
                102,
                6043,
                289
            });
    }
}


using Altium.SortFile;
using FluentAssertions;
using Xunit;

public class FileSorterTests
{
    [Fact]
    public void SortLargeFile_ShouldCreateChunks_WhenInputHasManyLines()
    {
        // Arrange
        string inputFilePath = "test_input.txt";
        string outputFilePath = "test_output.txt";
        int chunkSize = 3; // Small size for testing

        // Prepare a test input file
        File.WriteAllLines(inputFilePath, new[]
        {
                "Line 3. This is a line.",
                "Line 1. This is a line.",
                "Line 2. This is a line.",
                "Line 5. This is a line.",
                "Line 4. This is a line."
            });

        FileSorter fileSorter = new FileSorter();

        // Act
        fileSorter.SortLargeFile(inputFilePath, outputFilePath, chunkSize);

        // Assert
        File.Exists(outputFilePath).Should().BeTrue();
        var outputLines = File.ReadAllLines(outputFilePath);
        outputLines.Should().BeInAscendingOrder();

        // Cleanup
        File.Delete(inputFilePath);
        File.Delete(outputFilePath);
    }

    [Fact]
    public void CleanupTempFiles_ShouldDeleteChunkFiles_WhenCalled()
    {
        // Arrange
        var tempDirectory = "../Shared/Temp";
        Directory.CreateDirectory(tempDirectory);
        var chunkFilePath = Path.Combine(tempDirectory, "chunk_1.txt");
        File.WriteAllText(chunkFilePath, "Test line");

        var chunkFiles = new List<string> { chunkFilePath };
        FileSorter fileSorter = new FileSorter();

        // Act
        fileSorter.CleanupTempFiles(chunkFiles);

        // Assert
        File.Exists(chunkFilePath).Should().BeFalse();
        Directory.Exists(tempDirectory).Should().BeFalse();
    }
}
using Altium.SortFile;
using FluentAssertions;
using Xunit;

public class MergerTests
{
    [Fact]
    public void MergeChunks_ShouldCombineFilesInSortedOrder()
    {
        // Arrange
        var chunkFiles = new List<string>
            {
                "chunk_1.txt",
                "chunk_2.txt"
            };

        // Create chunk files for testing
        File.WriteAllLines(chunkFiles[0], new[] { "Line 1. A line.", "Line 2. A line." });
        File.WriteAllLines(chunkFiles[1], new[] { "Line 3. A line.", "Line 4. A line." });

        string outputFilePath = "merged_output.txt";

        // Act
        Merger.MergeChunks(chunkFiles, outputFilePath);

        // Assert
        File.Exists(outputFilePath).Should().BeTrue();
        var mergedLines = File.ReadAllLines(outputFilePath);
        mergedLines.Should().BeInAscendingOrder();

        // Cleanup
        foreach (var chunkFile in chunkFiles)
        {
            File.Delete(chunkFile);
        }
        File.Delete(outputFilePath);
    }
}
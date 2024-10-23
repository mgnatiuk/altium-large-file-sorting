# Altium File Processing

## Structure of the Sorting Application

### 1. **Main Method**
- **Responsibility**: Orchestrates the flow of the program.
- **Key Tasks**:
  - Starts and stops the stopwatch to measure execution time.
  - Calls the necessary methods to process the input file, sort chunks, and merge them.
  - Prints summary information, including the total chunks created, total lines processed, and the final execution time.

### 2. **FileProcessor**
- **Responsibility**: Handles reading, splitting, and sorting the input file into chunks.
- **Key Tasks**:
  - Reads the input file line by line.
  - Splits the lines into manageable chunks based on a specified size.
  - Sorts each chunk using the `ChunkSorter`.

### 3. **ChunkSorter**
- **Responsibility**: Sorts each chunk and saves it to disk.
- **Key Tasks**:
  - Sorts the lines within a chunk.
  - Saves the sorted chunk to a temporary file.

### 4. **ChunkMerger**
- **Responsibility**: Merges sorted chunks into the final output file.
- **Key Tasks**:
  - Reads the sorted chunk files.
  - Merges them into a single output file using a priority queue to maintain order.

### 5. **LineComparer**
- **Responsibility**: Contains logic for comparing lines.
- **Key Tasks**:
  - Extracts numeric values from lines for comparison.
  - Compares string portions of lines.
  - Determines the ordering based on both numeric and string comparisons.

### 6. **TempFileManager**
- **Responsibility**: Cleans up temporary chunk files after processing.
- **Key Tasks**:
  - Deletes temporary chunk files.
  - Removes the temporary directory used for storage during the sorting process.


## Projects

1. **Altium.Generator**  
   This project generates large text files for testing purposes. Each line of the file follows the format: *Number. String*.

   **Example:**
   ```txt
   415. Apple
   30432. Something something something
   1. Apple
   32. Cherry is the best
   2. Banana is yellow
   ```

   The project allows you to create a large test file with a specified number of lines, some of which will have the same string part for sorting tests.

2. **Altium.SortFile**  
   This project sorts the generated text files. The sorting is based on two criteria:
   1. The string part (`String`) is compared first.
   2. If the string parts are the same, the number (`Number`) is compared.

   **Example of sorted output:**
   ```txt
   1. Apple
   415. Apple
   2. Banana is yellow
   32. Cherry is the best
   30432. Something something something
   ```

3. **Altium.SortFile.Tests**  
   This project contains unit tests for the sorting logic. It ensures the correctness of sorting large files and validates that the sorting algorithm meets the expected requirements.

4. **Shared**  
   This folder is used to store temporary files and the generated test data files.

## Task Description
The input is a large text file where each line is in the format of `Number. String`. Both parts can be repeated. The goal is to generate and sort such files based on:
1. The string part.
2. The number part, if the string part matches.

The file size for testing can be up to 100GB.

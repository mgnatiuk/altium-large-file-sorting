# Altium File Processing

## Structure of the Sorting Application

### Program.cs
- **Purpose**: Entry point of the application. It initializes the sorting process by reading input parameters and invoking the `FileSorter`.

### FileSorter.cs
- **Purpose**: Manages the overall sorting process of the large file. This includes reading the input file, splitting it into manageable chunks, and coordinating the sorting and merging of these chunks.

### TemporaryFileManager.cs
- **Purpose**: Handles the creation and management of temporary files. This class is responsible for sorting the individual chunks, saving them to disk, and cleaning up after the sorting process is complete.

### LineComparer.cs
- **Purpose**: Contains the logic for comparing lines and sorting them. This class implements the custom comparison logic required for sorting the lines based on specified criteria.


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

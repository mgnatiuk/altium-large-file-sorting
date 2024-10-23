# Altium.SortFile Documentation

## Namespace: Altium.SortFile

The `Altium.SortFile` namespace contains classes designed for efficiently sorting large text files by processing them in chunks. The main classes include `Program`, `FileSorter`, `Sorter`, and `Merger`.

### 1. Program

The `Program` class serves as the entry point for the application. It initializes the sorting process by reading a specified input file, processing its contents, and outputting the sorted results to a designated output file.

### 2. FileSorter

The `FileSorter` class handles the core logic for sorting large files by reading them in chunks. It manages chunk creation, sorting, and merging of sorted chunks into the final output.

#### Fields

- **tempDirectory**: A string representing the directory path where temporary chunk files are stored.

#### Methods

- **SortLargeFile(string inputFilePath, string outputFilePath, int chunkSize)**
  - **Description**: Sorts a large text file by dividing it into smaller chunks, sorting those chunks, and then merging them into a single output file.
  - **Parameters**: 
    - `inputFilePath`: The path to the input file to be sorted.
    - `outputFilePath`: The path where the sorted output file will be saved.
    - `chunkSize`: The number of lines to read and sort per chunk.
  - **Exceptions**: Catches exceptions related to file handling and displays error messages.

- **CleanupTempFiles(List<string> chunkFiles)**
  - **Description**: Deletes temporary chunk files and the temporary directory after processing is complete.
  - **Parameters**: 
    - `chunkFiles`: A list of paths to the chunk files to be deleted.

### 3. Sorter

The `Sorter` class is responsible for sorting a list of lines based on specific criteria and saving the sorted lines to a chunk file.

#### Methods

- **SortAndSaveChunk(List<(string line, string textPart, int number)> lines, int chunkIndex, string tempDirectory)**
  - **Description**: Sorts a chunk of lines and saves them to a specified file.
  - **Parameters**: 
    - `lines`: A list of tuples containing the lines to sort along with their extracted parts.
    - `chunkIndex`: The index of the chunk being processed.
    - `tempDirectory`: The directory where the chunk file will be saved.
  - **Returns**: The path to the saved chunk file.

### 4. Merger

The `Merger` class is responsible for merging sorted chunk files back into a single sorted output file.

#### Methods

- **MergeChunks(List<string> chunkFiles, string outputFilePath)**
  - **Description**: Merges multiple sorted chunk files into a single output file while maintaining the overall sorted order.
  - **Parameters**: 
    - `chunkFiles`: A list of paths to the chunk files to merge.
    - `outputFilePath`: The path where the merged output file will be saved.
  - **Exceptions**: Catches exceptions related to file handling and displays error messages.

### 5. Extractor

The `Extractor` class contains the logic for extracting specific parts from each line of the input file, which is utilized by the `FileSorter` class. Ensure that this class is implemented correctly to support the sorting and merging processes.

## Projects

### 1. Altium.Generator

This project generates large text files for testing purposes. Each line of the file follows the format: *Number. String*.

#### Example Input:

   ```txt
   415. Apple
   30432. Something something something
   1. Apple
   32. Cherry is the best
   2. Banana is yellow
   ```

The project allows you to create a large test file with a specified number of lines, some of which will have the same string part for sorting tests.

### 2. Altium.SortFile

This project sorts the generated text files. The sorting is based on two criteria:
1. The string part (`String`) is compared first.
2. If the string parts are the same, the number (`Number`) is compared.

#### Example of Sorted Output:
   ```txt
   415. Apple
   1. Apple
   415. Apple
   2. Banana is yellow
  32. Cherry is the best
  30432. Something something something
   ```


### 3. Altium.SortFile.Tests

This project contains unit tests for the sorting logic. It ensures the correctness of sorting large files and validates that the sorting algorithm meets the expected requirements.

### 4. Shared

This folder is used to store temporary files and the generated test data files.

## Task Description

The input is a large text file where each line is in the format of `Number. String`. Both parts can be repeated. The goal is to generate and sort such files based on:
1. The string part.
2. The number part, if the string part matches.

The file size for testing can be up to **100GB**.

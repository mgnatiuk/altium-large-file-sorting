# Altium File Processing

## Overview
This repository consists of three main projects designed to handle large text files. The task involves generating test files and sorting them based on specific criteria.

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

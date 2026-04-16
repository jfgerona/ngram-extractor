# N-Gram Extractor

A C# console application that processes input text, builds adjacent word n-grams, looks them up in a subset of the WordNet lexical database, and writes the results to a `debug.txt` output file.

## Overview

This project was built for **INFO-3142** and focuses on basic natural language processing concepts such as tokenization, n-gram construction, dictionary-based lookup, and file processing.

The application reads an input text file from the command line, splits the text into sentences and word tokens, constructs n-grams from adjacent words, and searches for matching entries in the provided WordNet noun data. Matching n-grams are then written to an output report.

## Features

- Command-line input for selecting the source text file
- Tokenization of input text into sentences and words
- Generalized n-gram construction
- Support for 2-level, 3-level, and 4-level n-grams
- Dictionary key search or binary search for WordNet lookup
- Uses WordNet noun index and data files
- Outputs results to `debug.txt`
- Handles multiple sentences in a single input file
- Uses underscore-separated tokens to match WordNet formatting

## How It Works

1. The application loads the provided **WordNet noun index and noun data files** into memory.
2. The user runs the program from the command line and provides an input text filename.
3. The application reads the file and separates the content into sentences.
4. Each sentence is tokenized into individual words.
5. The program builds all adjacent word combinations for:
   - 2-grams
   - 3-grams
   - 4-grams
6. Each n-gram is converted to the WordNet format using underscores instead of spaces.
7. The program searches the noun index for matching tokens.
8. If a match is found, the corresponding definition is retrieved from the noun data file.
9. The final results are written to `debug.txt`.

## Example

Input sentence:

`I am flying to San Francisco to visit a venture capitalist.`

Possible generated n-grams include:

- `San_Francisco`
- `venture_capitalist`
- `I_am_flying`
- `visit_a_venture`
- `to_San_Francisco_to`

If an n-gram exists in the WordNet noun data, its definition is included in the output.

## Technologies Used

- C#
- .NET / .NET Core
- Visual Studio
- File I/O
- Dictionary-based search
- Basic NLP concepts

## Project Structure

A typical implementation for this project may include:

- `Program.cs`
  - Entry point and command-line handling
- file loading / parsing classes
  - Load WordNet noun index and noun data
- tokenization logic
  - Split text into sentences and words
- n-gram builder
  - Generate adjacent n-grams dynamically
- lookup service
  - Search n-grams in the WordNet noun index
- output writer
  - Write results to `debug.txt`

## What I Learned

This project helped me practice:

- command-line application development in C#
- working with large text-based datasets
- tokenization and text parsing
- building generalized n-gram logic instead of hard-coding
- optimizing lookup using in-memory structures like dictionaries
- connecting input data to reference data for semantic lookup
- generating structured debug output

## How to Run

1. Open the project in **Visual Studio**
2. Make sure the required WordNet noun files are included with the project
3. Build the solution
4. Run the application with a command-line argument, for example:

```bash
ngram test_text2.txt

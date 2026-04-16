// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;


Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
Dictionary<string, List<string>> indexDictionary = new Dictionary<string, List<string>>();
StreamWriter writeTxt;
string ? directory = Path.GetDirectoryName(path: AppDomain.CurrentDomain.BaseDirectory);
string text = "";
string? f_name = "";
string? f_name_out = "";
string file_path = "";
string file_path_out = "";

//Title
Console.WriteLine("Project #1 – N-gram Extractor - Version 1.0");

AddDictionaries();

//Read file
bool exists = true;
do
{
    Console.Write("\nPlease Enter the name of text file you want to read (excluding .txt): ");
    f_name = Console.ReadLine();
    file_path = directory + @$"/{f_name}.txt";
    if (File.Exists(file_path))
    {
        exists = true;
    }
    else
    {
        Console.WriteLine("File doesn't exist");
        exists = false;
    }
} while (!exists);

do
{
    Console.Write("\nPlease Enter the name of output file (excluding .txt): ");
    f_name_out = Console.ReadLine();
    file_path_out = directory + @$"/{f_name_out}.txt";
    if (File.Exists(file_path_out))
    {
        Console.WriteLine("File already exists!");
        exists = true;
    }
    else
    {
        exists = false;
    }
} while (exists);

//Create and write to file
writeTxt = new StreamWriter($"{f_name_out}.txt");

//Process
text = File.ReadAllText(file_path);
writeTofile(text + "\n");
string[] sentence = text.Split('.');
sentence = sentence.Take(sentence.Count() - 1).ToArray();
foreach (string s in sentence)
{
    writeTofile("\n" + s + "\n");
    //Parse the data
    parseData(s);
}

//Close file
writeTxt.Close();

Console.WriteLine("\n\n===================SUCCESS===================");
Console.WriteLine($"\nPlease open {f_name_out}.txt @{file_path_out}");

void parseData(string? data)
{
    List<string> definitions = new List<string>();
    string parsedData = "";
    int counter = 2;

    //2-gram parsing
    writeTofile($"\n{counter} level n-gram" + "\n");

    if(data[0].Equals(' '))
    {
        data = data.Substring(1);
    }
    string[] data2 = data.Split(" ");
    bool isDefiniton = false;
    for (int i = 0; i < data2.Length; i++)
    {
        string second = "";
        if(i != data2.Length - 1)
        {
            second = data2[i + 1];
        }
        if(i > 0 && isDefiniton == false)
        {
            writeTofile(parsedData + ",");
        }
        isDefiniton = false;
        parsedData = data2[i] + "_" + second;
        List<string>? index = getIndex(parsedData);
        if (index != null)
        {
            //Get the definition from the index
            definitions = getDefinitions(index);
            foreach(string d in definitions)
            {
                writeTofile($"{parsedData}, {d}");
                isDefiniton = true;
            }
        }
        else
        {
            continue;
        }
    }
    counter++;
    //3-gram parsing
    writeTofile($"\n{counter} level n-gram" + "\n");

    for(int i = 0; i < data2.Length-1; i++)
    {
        string second = "";
        string third = "";
        if(i != data2.Length - 2)
        {
            second = data2[i + 1];
            third = data2[i + 2];
        }
        if (i > 0 && isDefiniton == false)
        {
            writeTofile(parsedData + ",");
        }
        isDefiniton = false;
        parsedData = data2[i] + "_" + second + "_" + third;
        List<string>? index = getIndex(parsedData);
        if (index != null)
        {
            //Get the definition from the index
            definitions = getDefinitions(index);
            foreach (string d in definitions)
            {
                writeTofile($"{parsedData}, {d}");
                isDefiniton = true;
            }
        }
        else
        {
            continue;
        }

    }
    counter++;
    //4-gram parsing
    writeTofile($"\n{counter} level n-gram" + "\n");

    for (int i = 0; i < data2.Length - 2; i++)
    {
        string second = "";
        string third = "";
        string fourth = "";
        if (i != data2.Length - 3)
        {
            second = data2[i + 1];
            third = data2[i + 2];
            fourth = data2[i + 3];
        }
        if (i > 0 && isDefiniton == false)
        {
            writeTofile(parsedData + ",");
        }
        isDefiniton = false;
        parsedData = data2[i] + "_" + second + "_" + third + "_" + fourth;
        List<string>? index = getIndex(parsedData);
        if (index != null)
        {
            //Get the definition from the index
            definitions = getDefinitions(index);
            foreach (string d in definitions)
            {
                writeTofile($"{parsedData}, {d}");
                isDefiniton = true;
            }
        }
        else
        {
            continue;
        }

    }
}

void AddDictionaries()
{
    string nounsDataFilePath = directory + "/NounsData.txt";
    string nounsIndexFilePath = directory + "/NounsIndex.txt";
    if (!File.Exists(nounsDataFilePath) || !File.Exists(nounsIndexFilePath))
    {
        Console.WriteLine("\nNounsData.txt\\NounsIndex.txt could not be found. Please provide data with executable.");
        System.Environment.Exit(1);
    }

    string data = File.ReadAllText(nounsDataFilePath);
    string index = File.ReadAllText(nounsIndexFilePath);

    string[] dataLine = data.Split(new string[] { Environment.NewLine },
    StringSplitOptions.None);

    foreach(string d in dataLine)
    {
        string[] keyValue = d.Split('|');
        dataDictionary.Add(keyValue.First(), keyValue.Last());
    }

    string[] indexLine = index.Split(new string[] { Environment.NewLine },
    StringSplitOptions.None);

    foreach (string i in indexLine)
    {
        string[] keyValue = i.Split('|');
        string[] indexNumbers = keyValue.Last().Split(',');
        indexDictionary.Add(keyValue.First(), indexNumbers.ToList<string>());
    }
}

List<string> getDefinitions(List<string> index)
{
    List<string> definitions = new List<string>();
    string desc = "";
    foreach(string i in index)
    {
        dataDictionary.TryGetValue(i, out desc);
        definitions.Add(desc);
    }
    return definitions;
}

List<string>? getIndex(string data)
{
    List<string>? idxNum = new List<string>();
    if(indexDictionary.TryGetValue(data.ToLower(), out idxNum))
    {
        return idxNum;
    };
    return null;
}

void writeTofile(string data_in)
{
    writeTxt.WriteLine(data_in);
}
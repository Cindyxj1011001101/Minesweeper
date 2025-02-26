using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader:Singleton<CSVReader>
{
    public static List<List<BlockType>> ReadBlockTypeCSV(TextAsset csvFile)
    {
        List<List<BlockType>> data = new List<List<BlockType>>();
        string[] lines = csvFile.text.Split('\n');
        foreach (string line in lines)
        {
            List<BlockType> row = new List<BlockType>();
            string[] values = line.Split(',');
            foreach (string value in values)
            {
                if (value != "")
                {
                    row.Add((BlockType)int.Parse(value));
                }
            }
            data.Add(row);
        }
        return data;
    }
}
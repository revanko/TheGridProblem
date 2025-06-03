// DataAccess.cs (Data Access Layer)
// --------------
// Responsible for presenting the grid data to the console
//
// Author: Ron Evanko
// Created: 5/31/2025
using TheGridProblem.Models;

namespace TheGridProblem.DAL
{
    public static class DataAccess
    {
        public static List<T> LoadFromFile<T>(string path) where T : ICustomParsable<T>
        {
            var items = new List<T>();

            if (!File.Exists(path))
                throw new FileNotFoundException($"File not found: {path}");

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                if (T.TryParse(trimmed, out var item))
                {
                    items.Add(item!);
                }
                else
                {
                    throw new FormatException($"Invalid line in input: '{line}'");
                }
            }

            return items;
        }
    }
}

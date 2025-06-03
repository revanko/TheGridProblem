// GridView.cs
// --------------
// Responsible for presenting the grid data to the console
//
// Author: Ron Evanko
// Created: 5/31/2025

using TheGridProblem.Models;

namespace TheGridProblem.Views
{
    public static class GridView
    {
        public static void Show(GridData data)
        {
            // Display the rows.
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Console.WriteLine($"Row {i}: " + string.Join(" - ", data.Rows[i].Select(p => $"{p.X:0.0},{p.Y:0.0}")));
            }

            // Display the columns.
            for (int i = 0; i < data.Columns.Count; i++)
            {
                Console.WriteLine($"Col {i}: " + string.Join(" - ", data.Columns[i].Select(p => $"{p.X:0.0},{p.Y:0.0}")));
            }

            // Display the degrees.
            Console.WriteLine($"Alpha={data.AlphaDegrees:0.0} degrees");
            Console.ReadLine();
        }
    }
}

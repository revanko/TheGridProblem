// GridModel.cs
// ------------------
// Represents a 2D coordinate for the grid point.
// Implements ICustomParsable for file loading.
//
// Author: Ron Evanko
// Updated: 5/31/2025

using System;
using TheGridProblem.Models;

namespace TheGridProblem.Models
{
    public class GridModel : ICustomParsable<GridModel>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public GridModel(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static bool TryParse(string input, out GridModel? point)
        {
            point = null;
            var parts = input.Split(',');
            if (parts.Length != 2) return false;

            if (double.TryParse(parts[0], out var x) && double.TryParse(parts[1], out var y))
            {
                point = new GridModel(x, y);
                return true;
            }

            return false;
        }
    }
}

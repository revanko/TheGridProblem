// GridData.cs
// ------------------
// Process data coordinates assuming points form an approximate square grid.
// Determines rows and columns based on sorted coordinates and square root logic.
// Rotation is computed as an angle relative to X-axis.
//
// Author: Ron Evanko
// Created: 5/31/2025

namespace TheGridProblem.Models
{
    public class GridData
    {
        public List<GridModel> Points { get; set; } = new();
        public List<List<GridModel>> Rows { get; private set; } = new();
        public List<List<GridModel>> Columns { get; private set; } = new();
        public double AlphaDegrees { get; private set; } = 0.0;

        public void ProcessGrid()
        {
            // Estimate size of grid (to determine number of rows and columns).
            int gridSize = (int)Math.Sqrt(Points.Count);
            if (gridSize * gridSize != Points.Count)
                throw new InvalidOperationException("Point count must form a square grid.");

            // Sort by Y descending (top to bottom), then X ascending (left to right)
            var sorted = Points
                .OrderByDescending(p => p.Y)
                .ThenBy(p => p.X)
                .ToList();

            // Fill rows
            Rows = new();
            for (int i = 0; i < sorted.Count; i += gridSize)
            {
                var row = sorted.Skip(i).Take(gridSize).ToList();
                row.Sort((a, b) => a.X.CompareTo(b.X));
                Rows.Add(row);
            }

            // Fill (transpose from rows) columns (top to bottom, then ensure descending row order)
            Columns = new();
            for (int col = 0; col < gridSize; col++)
            {
                var column = new List<GridModel>();
                for (int row = 0; row < gridSize; row++)
                {
                    column.Add(Rows[row][col]);
                }
                // Sort in descending row order (i.e., top to bottom → highest to lowest Y)
                column.Sort((a, b) => b.Y.CompareTo(a.Y));
                Columns.Add(column);
            }

            // Compute rotation angle (Alpha)
            if (Rows.Count > 1 && Rows[0].Count > 1)
            {
                var dx = Rows[0][1].X - Rows[0][0].X;
                var dy = Rows[0][1].Y - Rows[0][0].Y;
                AlphaDegrees = Math.Atan2(dy, dx) * (180.0 / Math.PI);
            }
        }
    }
}

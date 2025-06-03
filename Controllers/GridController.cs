// GridController.cs
// ------------------
// Simplified controller logic assuming points form an approximate square grid.
// Rows and columns are inferred using square root of point count.
// Rotation is accounted for in classification.
//
// Author: Ron Evanko
// Created: 5/31/2025

using TheGridProblem.Models;

namespace TheGridProblem.Controllers
{
    public class GridController
    {
        public GridData ProcessFile(string filePath)
        {
            // Load the data from the incoming file.
            var points = DAL.DataAccess.LoadFromFile<GridModel>(filePath);

            // Initialize GridData.
            var gridData = new GridData { Points = points };

            // Organize data coordinates into rows & columns and determine angle/degree of the grid.
            gridData.ProcessGrid(); 

            return gridData;
        }
    }
}

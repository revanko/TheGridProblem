// Program.cs
// ------------------
// Application entry point that loads a coordinate file, validates it,
// and renders the square grid via the controller and view layers.
//
// Author: Ron Evanko
// Updated: 5/31/2025

using TheGridProblem.Controllers;
using TheGridProblem.Models;
using TheGridProblem.Views;

#if DEBUG
// Simulate argument in debug mode.
if (args.Length == 0)
{
    // Debug Test Files
    //args = new string[1]; // Allocate space

    //args[0] = @".\TestData\grid_input.txt";
    //args[0] = @".\TestData\grid_example.txt";
    //args[0] = @".\TestData\grid_5x5_25deg_quadrant_II.txt";
    //args[0] = @".\TestData\grid_5x5_neg25deg_quadrant_II.txt";
    //args[0] = @".\TestData\grid_4x4_35deg_quadrant_III.txt";
    //args[0] = @".\TestData\grid_4x4_quadrant_III.txt";
    //args[0] = @".\TestData\grid_6x6_10deg_quadrant_IV.txt";
    //args[0] = @".\TestData\grid_6x6_neg10deg_quadrant_IV.txt";

    //args[0] = @".\TestData\overlapping_x_large.txt";
    //args[0] = @".\TestData\overlapping_y_large.txt";

    //args[0] = @".\TestData\No File.txt";
    //args[0] = @".\TestData\Empty.txt";
    //args[0] = @".\TestData\Invalid_Data.txt";
}
#endif

// Check if a file argument was passed
if (args.Length == 0)
{
    Console.WriteLine("Error: No file argument provided.");
    Console.WriteLine(@"Usage: TheGridProblem [Path\Filename to Data-File]");
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
    return;
}

// Give a a more descriptive name to the incoming data file.
string dataFilePath = args[0];

if (!File.Exists(dataFilePath))
{
    Console.WriteLine($"Error: File does not exist: {dataFilePath}");
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
    return;
}

// Validate contents of data-file
List<GridModel> points;
try
{
    // Check to make sure data-file is not empty.
    var lines = File.ReadAllLines(dataFilePath);
    if (lines.Length == 0)
    {
        Console.WriteLine("Error: File contains no data.");
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
        return;
    }

    // Validate the formatting and data within the data-file.
    points = new List<GridModel>();
    foreach (var line in lines)
    {
        if (!GridModel.TryParse(line, out var point))
        {
            Console.WriteLine($"Error: Invalid line format in data-file: '{line}'");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
            return;
        }
        points.Add(point);
    }
}
catch (UnauthorizedAccessException)
{
    Console.WriteLine("Error: Access to file denied.");
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
    return;
}
catch (IOException ex)
{
    Console.WriteLine($"File read error: {ex.Message}");
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
    return;
}

// Process the data from the incoming file.
var controller = new GridController();
var data = controller.ProcessFile(dataFilePath);

// Display the results.
GridView.Show(data);
using System;

/*
 * A maze generator. Slightly modified logic from https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity.
 */
public class MazeGenerator
{
    /// <summary>
    /// Create a maze of <c>size</c> with <c>seed</c>. Create an empty room at <c>emptyPosition</c> of <c>emptySize</c>.
    /// </summary>
    /// <param name="size">Side length.</param>
    /// <param name="seed">Randomizer seed.</param>
    /// <param name="emptyPosition">Position of empty room.</param>
    /// <param name="emptySize">Empty room size.</param>
    /// <returns>Boolean matrix where [row, col] is true if there is a wall.</returns>
    public bool[,] GenerateMaze(int size, int seed, int emptyPosition, int emptySize)
    {
        Random rand = new Random(seed);

        float placementThreshold = .4f;

        bool[,] maze = new bool[size, size];

        int rowMax = maze.GetUpperBound(0);
        int columnMax = maze.GetUpperBound(1);

        for (int row = 0; row <= rowMax; row++) {
            for (int col = 0; col <= columnMax; col++) {
                if (row < emptyPosition + emptySize && row > emptyPosition - emptySize &&
                    col < emptyPosition + emptySize && col > emptyPosition - emptySize) {
                    continue;
                }

                if (row == 0 || col == 0 || row == rowMax || col == columnMax) {
                    // Create the borders
                    maze[row, col] = true;
                } else if (row % 2 == 0 && col % 2 == 0) {
                    if (rand.NextDouble() > placementThreshold) {
                        maze[row, col] = true;

                        int a = rand.NextDouble() < .5 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        maze[row + a, col + b] = true;
                    }
                }
            }
        }

        // Generate one opening in the maze sides
        int side = rand.Next(4);

        int openingRow = 0;
        int openingColumn = 0;

        while (openingRow == 0 && openingColumn == 0) { // top left corner is invalid for an opening
            int distance = rand.Next(1, size - 1);

            // Check that the opening isn't toward an interior wall

            if (side == 0 && !maze[1, distance]) {
                openingRow = 0;
                openingColumn = distance;
            } else if (side == 1 && !maze[rowMax - 1, distance]) {
                openingRow = rowMax;
                openingColumn = distance;
            } else if (side == 2 && !maze[distance, 1]) {
                openingRow = distance;
                openingColumn = 0;
            } else if (side == 3 && !maze[distance, columnMax - 1]) {
                openingRow = distance;
                openingColumn = columnMax;
            }
        }

        maze[openingRow, openingColumn] = false;

        return maze;
    }

    /// <summary>
    /// Create a maze of <c>size</c> with <c>seed</c>. Create an empty room in the middle that is 3*3 blocks.
    /// </summary>
    /// <param name="size">Side length.</param>
    /// <param name="seed">Randomizer seed.</param>
    /// <returns>Boolean matrix where [row, col] is true if there is a wall.</returns>
    public bool[,] GenerateMaze(int size, int seed)
    {
        return GenerateMaze(size, seed, size / 2, 3);
    }
}

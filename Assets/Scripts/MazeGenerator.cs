using System;

/*
 * A maze generator. Slightly modified logic from https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity.
 */
public class MazeGenerator
{
    private int size;

    private float bombChance;
    private float fireChance;

    private int emptyPosition;
    private int emptySize;
    private Random rand;
    private float placementThreshold = .4f;

    public enum MazeObject
    {
        Ground,
        Wall,
        Bomb,
        Fire,
    }

    public MazeGenerator Size(int size)
    {
        this.size = size;
        return this;
    }

    public MazeGenerator Seed(int seed)
    {
        rand = new Random(seed);
        return this;
    }

    public MazeGenerator BombChance(float chance)
    {
        bombChance = chance;
        return this;
    }

    public MazeGenerator FireChance(float chance)
    {
        fireChance = chance;
        return this;
    }

    /// <summary>
    /// Set dimensions for an empty room.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public MazeGenerator EmptyRoom(int pos, int size)
    {
        emptyPosition = pos;
        emptySize = size;
        return this;
    }

    /// <summary>
    /// Build a maze with previously passed parameters.
    /// </summary>
    /// <returns>Matrix where [row, col] points to a MazeObject.</returns>
    public MazeObject[,] Build()
    {
        MazeObject[,] maze = new MazeObject[size, size];

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
                    maze[row, col] = MazeObject.Wall;
                } else if (row % 2 == 0 && col % 2 == 0) {
                    if (rand.NextDouble() > placementThreshold) {
                        maze[row, col] = MazeObject.Wall;

                        int a = rand.NextDouble() < .5 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        maze[row + a, col + b] = MazeObject.Wall;
                    }
                } else if (rand.NextDouble() < bombChance) {
                    maze[row, col] = MazeObject.Bomb;
                } else if (rand.NextDouble() < fireChance) {
                    maze[row, col] = MazeObject.Fire;
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

            if (side == 0 && maze[1, distance] != MazeObject.Wall) {
                openingRow = 0;
                openingColumn = distance;
            } else if (side == 1 && maze[rowMax - 1, distance] != MazeObject.Wall) {
                openingRow = rowMax;
                openingColumn = distance;
            } else if (side == 2 && maze[distance, 1] != MazeObject.Wall) {
                openingRow = distance;
                openingColumn = 0;
            } else if (side == 3 && maze[distance, columnMax - 1] != MazeObject.Wall) {
                openingRow = distance;
                openingColumn = columnMax;
            }
        }

        maze[openingRow, openingColumn] = MazeObject.Ground;

        return maze;
    }
}

﻿using System;

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

        double wallChance = 0.7;

        // Randomly create openings in the outer borders

        for (int col = 0; col <= columnMax; col++) {
            if (!maze[1, col] && rand.NextDouble() > wallChance) {
                maze[0, col] = false;
            }

            if (!maze[rowMax - 1, col] && rand.NextDouble() > wallChance) {
                maze[rowMax, col] = false;
            }
        }

        for (int row = 0; row <= rowMax; row++) {
            if (!maze[row, 1] && rand.NextDouble() > wallChance) {
                maze[row, 0] = false;
            }

            if (!maze[row, columnMax - 1] && rand.NextDouble() > wallChance) {
                maze[row, columnMax] = false;
            }
        }

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

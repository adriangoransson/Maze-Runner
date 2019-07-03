using System;

public class MazeGenerator
{
    // Slightly modified logic from https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity
    public int[,] GenerateMaze(int size, int seed, int emptyPosition, int emptySize)
    {
        Random rand = new Random(seed);

        float placementThreshold = .4f;

        int[,] maze = new int[size, size];

        int rowMax = maze.GetUpperBound(0);
        int columnMax = maze.GetUpperBound(1);

        for (int row = 0; row <= rowMax; row++) {
            for (int col = 0; col <= columnMax; col++) {
                if (row < emptyPosition + emptySize && row > emptyPosition - emptySize &&
                    col < emptyPosition + emptySize && col > emptyPosition - emptySize) {
                    continue;
                }

                //1
                if (row == 0 || col == 0 || row == rowMax || col == columnMax && rand.NextDouble() < .3) {
                    maze[row, col] = 1;
                }

                //2
                else if (row % 2 == 0 && col % 2 == 0) {
                    if (rand.NextDouble() > placementThreshold) {
                        //3
                        maze[row, col] = 1;

                        int a = rand.NextDouble() < .5 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        maze[row + a, col + b] = 1;
                    }
                }
            }
        }

        return maze;
    }

    // Create a maze of `size` with `seed`. Create an empty room in the middle that is 3*3 blocks.
    public int[,] GenerateMaze(int size, int seed)
    {
        return GenerateMaze(size, seed, size / 2, 3);
    }
}

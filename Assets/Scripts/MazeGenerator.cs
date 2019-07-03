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

        for (int i = 0; i <= rowMax; i++) {
            for (int j = 0; j <= columnMax; j++) {
                if (i < emptyPosition + emptySize && i > emptyPosition - emptySize &&
                    j < emptyPosition + emptySize && j > emptyPosition - emptySize) {
                    continue;
                }

                //1
                if (i == 0 || j == 0 || i == rowMax || j == columnMax && rand.NextDouble() < .3) {
                    maze[i, j] = 1;
                }

                //2
                else if (i % 2 == 0 && j % 2 == 0) {
                    if (rand.NextDouble() > placementThreshold) {
                        //3
                        maze[i, j] = 1;

                        int a = rand.NextDouble() < .5 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (rand.NextDouble() < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }

        return maze;
    }

    // Create a maze of `size` with `seed`. Create an empty room in the middle that is 5*5 blocks.
    public int[,] GenerateMaze(int size, int seed)
    {
        return GenerateMaze(size, seed, size / 2, 3);
    }
}

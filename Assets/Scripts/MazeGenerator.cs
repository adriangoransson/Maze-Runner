using System;

public class MazeGenerator
{
    // Slightly modified logic from https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity
    public int[,] GenerateMaze(int size, int seed)
    {
        Random rand = new Random(seed);

        float placementThreshold = .4f;

        int[,] maze = new int[size, size];

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++) {
            for (int j = 0; j <= cMax; j++) {
                //1
                if (i == 0 || j == 0 || i == rMax || j == cMax && rand.NextDouble() < .3) {
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
}

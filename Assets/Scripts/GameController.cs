using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Wall;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateMaze()
    {
        MazeGenerator mg = new MazeGenerator();

        int size = 50;

        int[,] maze = mg.GenerateMaze(size, 1230123);

        for (int row = 0; row < maze.GetLength(0); row++) {
            int x = row;

            for (int column = 0; column < maze.GetLength(1); column++) {
                int z = column;

                if (maze[row, column] == 1) {
                    Vector3 pos = new Vector3(x - (size / 2), Wall.transform.position.y, z - (size / 2));
                    Instantiate(Wall, pos, Quaternion.identity);
                }
            }
        }
    }
}

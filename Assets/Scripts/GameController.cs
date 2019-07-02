using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Wall;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator mg = new MazeGenerator();

        int[,] maze = mg.GenerateMaze(30, 1230123);

        for (int row = 0; row < maze.GetLength(0); row++) {
            int x = row;

            for (int column = 0; column < maze.GetLength(1); column++) {
                int z = column;

                if (maze[row, column] == 1) {
                    Vector3 pos = new Vector3(x, Wall.transform.position.y, z);
                    Instantiate(Wall, pos, Quaternion.identity);
                }
            }
        }

        Vector3 v = new Vector3(0, 0, 0);
        Instantiate(Wall, v, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

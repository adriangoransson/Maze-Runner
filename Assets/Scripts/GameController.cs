using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Wall;
    public GameObject boundary;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateMaze(50);
        SetBounds(50);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateMaze(int size)
    {
        MazeGenerator mg = new MazeGenerator();
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

    private void SetBounds(int size)
    {
        BoxCollider b = boundary.GetComponent<BoxCollider>();
        b.size = new Vector3(size, size, size);
    }
}

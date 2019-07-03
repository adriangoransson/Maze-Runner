using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Wall;
    public GameObject boundary;
    public GameObject timer;
    public GameObject menu;
    public Button menuButton;

    private int size;
    private int seed;

    private float time;
    private Text timerText;
    private bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
        size = PlayerPrefs.GetInt(MainMenuController.SIZE_KEY);
        seed = PlayerPrefs.GetInt(MainMenuController.SEED_KEY);

        InstantiateMaze();
        SetBounds();

        time = 0;
        timerText = timer.GetComponent<Text>();
        gameIsPaused = false;
        menu.SetActive(false);

        menuButton.onClick.AddListener(TogglePause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        } else if (!gameIsPaused) {
            time += Time.deltaTime;
            timerText.text = SecondsToString((int)time);
        }
    }

    public void TogglePause()
    {
        if (gameIsPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    void Pause()
    {
        print("Pause");
        menu.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    void Resume()
    {
        print("Resume");
        menu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1;
    }

    public void AddPenalty()
    {
        time += 5;
    }

    public void WinGame()
    {
        print("Win");
    }

    public void GameOver()
    {
        print("Game over");
    }

    private void InstantiateMaze()
    {
        MazeGenerator mg = new MazeGenerator();
        int[,] maze = mg.GenerateMaze(size, seed);

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

    private void SetBounds()
    {
        BoxCollider b = boundary.GetComponent<BoxCollider>();
        b.size = new Vector3(size, size, size);
    }

    private string SecondsToString(int seconds)
    {
        int minutes = seconds / 60;
        seconds = seconds % 60;

        string min = minutes > 9 ? minutes.ToString() : "0" + minutes;
        string sec = seconds > 9 ? seconds.ToString() : "0" + seconds;

        return min + ":" + sec;
    }
}

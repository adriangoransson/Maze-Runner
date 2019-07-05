using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Wall;
    public GameObject boundary;
    public GameObject timer;
    public GameObject menu;
    public Button menuButton;

    public GameObject beforeStartText;
    public GameObject gameOverText;
    public GameObject winText;

    private int size;
    private int seed;

    private float time;
    private Text timerText;
    private bool gameHasBegun;
    private bool gameIsPaused;
    private bool gameIsOver;
    private bool gameIsWon;

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

        Time.timeScale = 0;

        beforeStartText.SetActive(true);
        gameOverText.SetActive(false);
        winText.SetActive(false);
        menu.SetActive(false);

        menuButton.onClick.AddListener(TogglePause);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasBegun) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                gameHasBegun = true;
                beforeStartText.SetActive(false);
                Time.timeScale = 1;
            }
        } else if (gameIsOver || gameIsWon) {
            Time.timeScale = 0;

            if (gameIsOver) {
                gameOverText.SetActive(true);
            } else {
                winText.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);
            }
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }

        if (gameIsPaused) {
            return;
        }


        time += Time.deltaTime;
        timerText.text = SecondsToString((int)time);
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
        menu.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    void Resume()
    {
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
        gameIsWon = true;
    }

    public void GameOver()
    {
        gameIsOver = true;
    }

    private void InstantiateMaze()
    {
        MazeGenerator mg = new MazeGenerator();
        bool[,] maze = mg.GenerateMaze(size, seed);

        for (int row = 0; row < maze.GetLength(0); row++) {
            int x = row - size / 2;

            for (int column = 0; column < maze.GetLength(1); column++) {
                int z = column - size / 2;

                if (maze[row, column]) {
                    Vector3 pos = new Vector3(x, Wall.transform.position.y, z);
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

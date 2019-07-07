using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Main game controller. Responsible for creating the game world and checking win conditions.
 */
public class GameController : MonoBehaviour
{
    public int timePenalty = 5;

    public GameObject wall;
    public GameObject timer;
    public GameObject menu;
    public GameObject menuButton;
    public BoxCollider boundary;

    public GameObject beforeStartText;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject newHighscoreText;
    public GameObject bestScoreText;

    private int size;
    private int seed;

    private float time;
    private Text timerText;
    private bool gameHasBegun;
    private bool gameIsPaused;
    private bool gameIsOver;
    private bool gameIsWon;

    void Start()
    {
        Resume();
        size = PlayerPrefs.GetInt(MainMenuController.SIZE_KEY);
        seed = PlayerPrefs.GetInt(MainMenuController.SEED_KEY);

        InstantiateMaze();
        SetBounds();

        time = 0;
        timerText = timer.GetComponent<Text>();

        // Game is initially paused.
        Time.timeScale = 0;

        beforeStartText.SetActive(true);
        gameOverText.SetActive(false);
        winText.SetActive(false);
        menu.SetActive(false);
        menuButton.SetActive(false);
    }

    void Update()
    {
        if (!gameHasBegun) {
            // Wait for player to press space before starting.
            if (Input.GetKeyDown(KeyCode.Space)) {
                gameHasBegun = true;
                beforeStartText.SetActive(false);
                Time.timeScale = 1;
            }
        } else if (gameIsOver || gameIsWon) {
            // Press R to restart or Esc for main menu.
            if (Input.GetKeyDown(KeyCode.R)) {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(0);
            }
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        } else {
            // Ordinary game loop.
            menuButton.SetActive(true);

            time += Time.deltaTime;
            timerText.text = Utils.SecondsToString((int)time);
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

    /*
     * Win or game over state.
     */
    void EndGame()
    {
        // There is text on screen so menu is unavailable.
        menuButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void AddPenalty()
    {
        time += timePenalty;
    }

    public void WinGame()
    {
        HighscoreDataManager hsm = new HighscoreDataManager();

        string level = size + "." + seed;

        Highscore hs = hsm.GetScoreForLevel(level);
        int bestTime = (int)time;

        if (hs != null) {
            bestTime = Mathf.Min(hs.Seconds, bestTime);
        }

        if (hs == null || hs.Seconds > time) {
            hsm.Save(level, (int)time);
            newHighscoreText.SetActive(true);
        }

        bestScoreText.GetComponent<Text>().text += " " + Utils.SecondsToString(bestTime);

        bestScoreText.SetActive(true);
        winText.SetActive(true);
        gameIsWon = true;
        EndGame();
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        gameIsOver = true;
        EndGame();
    }

    /*
     * Instantiate wall prefabs according to matrix from MazeGenerator.
     */
    private void InstantiateMaze()
    {
        MazeGenerator mg = new MazeGenerator();
        bool[,] maze = mg.GenerateMaze(size, seed);

        for (int row = 0; row < maze.GetLength(0); row++) {
            int x = row - size / 2;

            for (int column = 0; column < maze.GetLength(1); column++) {
                int z = column - size / 2;

                if (maze[row, column]) {
                    Vector3 pos = new Vector3(x, wall.transform.position.y, z);
                    Instantiate(wall, pos, Quaternion.identity);
                }
            }
        }
    }

    /*
     * Set size for the bounds checker to match the maze.
     */
    private void SetBounds()
    {
        boundary.size = new Vector3(size, size, size);
    }
}

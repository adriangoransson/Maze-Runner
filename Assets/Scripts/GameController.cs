using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Main game controller. Responsible for creating the game world and checking win conditions.
 */
public class GameController : MonoBehaviour
{
    public int timePenaltyOnCollision = 5;
    public int timeDecreaseOnBonus = 10;

    public GameObject bomb;
    [Range(0, 1)]
    public float bombChance;

    public GameObject fire;
    [Range(0, 1)]
    public float fireChance;

    public GameObject clock;
    [Range(0, 1)]
    public float clockChance;

    public GameObject jumpSymbol;
    [Range(0, 1)]
    public float jumpChance;

    public GameObject[] walls;

    public GameObject menu;
    public GameObject menuButton;
    public Text timerText;
    public Text bestScoreText;
    public Text jumpsText;
    public BoxCollider boundary;

    public GameObject beforeStartText;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject newHighscoreText;

    private int size;
    private int seed;

    private float time;
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
        time += timePenaltyOnCollision;
    }

    public void DecreaseTime()
    {
        time -= timeDecreaseOnBonus;
    }

    public void SetJumps(int jumps)
    {
        jumpsText.text = "Jumps: " + jumps;
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

        bestScoreText.text += " " + Utils.SecondsToString(bestTime);
        bestScoreText.gameObject.SetActive(true);

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

        MazeGenerator.MazeObject[,] maze = mg
            .Size(size)
            .Seed(seed)
            .EmptyRoom(size / 2, 3)
            .BombChance(bombChance)
            .FireChance(fireChance)
            .ClockChance(clockChance)
            .JumpChance(jumpChance)
            .Build();

        System.Random rand = new System.Random(seed);
        for (int row = 0; row < maze.GetLength(0); row++) {
            int x = row - size / 2;

            for (int column = 0; column < maze.GetLength(1); column++) {
                int z = column - size / 2;

                Vector3 pos;
                switch (maze[row, column]) {
                    case MazeGenerator.MazeObject.Wall:
                        GameObject wall = walls[rand.Next(walls.Length)];
                        pos = new Vector3(x, wall.transform.position.y, z);
                        Instantiate(wall, pos, Quaternion.identity);
                        break;
                    case MazeGenerator.MazeObject.Bomb:
                        pos = new Vector3(x, bomb.transform.position.y, z);
                        Instantiate(bomb, pos, bomb.transform.rotation);
                        break;
                    case MazeGenerator.MazeObject.Fire:
                        pos = new Vector3(x, fire.transform.position.y, z);
                        Instantiate(fire, pos, Quaternion.identity);
                        break;
                    case MazeGenerator.MazeObject.Clock:
                        pos = new Vector3(x, clock.transform.position.y, z);
                        Instantiate(clock, pos, Quaternion.identity);
                        break;
                    case MazeGenerator.MazeObject.Jump:
                        pos = new Vector3(x, jumpSymbol.transform.position.y, z);
                        Instantiate(jumpSymbol, pos, Quaternion.identity);
                        break;
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

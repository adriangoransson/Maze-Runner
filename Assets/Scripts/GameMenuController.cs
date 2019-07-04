using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{

    private GameController gameController;

    public Button resumeButton;
    public Button restartButton;
    public Button audioButton;
    public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        gameController = go.GetComponent<GameController>();

        resumeButton.onClick.AddListener(gameController.TogglePause);
        restartButton.onClick.AddListener(RestartGame);

        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

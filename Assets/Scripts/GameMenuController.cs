using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{

    private GameController gameController;

    public Button resumeButton;
    public Button newGameButton;
    public Button audioButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        gameController = go.GetComponent<GameController>();

        resumeButton.onClick.AddListener(gameController.TogglePause);
        newGameButton.onClick.AddListener(NewGame);

        quitButton.onClick.AddListener(Quit);
    }

    void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}

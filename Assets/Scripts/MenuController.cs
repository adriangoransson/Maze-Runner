using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
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

        quitButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {

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

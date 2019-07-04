using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public const string SIZE_KEY = "SIZE_KEY";
    public const string SEED_KEY = "SEED_KEY";

    private const int EASY = 20;
    private const int MEDIUM = 40;
    private const int HARD = 60;

    public InputField levelInput;
    public Button easyButton, mediumButton, hardButton, newSeedButton, playButton, highScoreButton, quitButton;

    private int size;
    private int seed;

    // Start is called before the first frame update
    void Start()
    {
        easyButton.onClick.AddListener(() => SetSize(EASY));
        mediumButton.onClick.AddListener(() => SetSize(MEDIUM));
        hardButton.onClick.AddListener(() => SetSize(HARD));
        newSeedButton.onClick.AddListener(NewSeed);
        playButton.onClick.AddListener(StartGame);

        highScoreButton.onClick.AddListener(OpenHighScores);
        quitButton.onClick.AddListener(Quit);

        levelInput.onValueChanged.AddListener(InputChanged);

        size = PlayerPrefs.GetInt(SIZE_KEY, MEDIUM);
        seed = PlayerPrefs.GetInt(SEED_KEY, GetNewSeed());
        UpdateTextField();
    }

    private void InputChanged(string input)
    {
        if (input.Equals("")) {
            return;
        }

        string[] args = input.Split('.');

        try {
            size = Convert.ToInt32(args[0]);

        } catch (Exception) {
            size = MEDIUM;
        }

        if (args.GetLength(0) < 2) {
            return;
        }

        try {
            seed = Convert.ToInt32(args[1]);

        } catch (Exception) {
            seed = GetNewSeed();
        }
    }

    private void SetSelectedButton()
    {
    }

    private void SetSize(int newSize)
    {
        size = newSize;
        UpdateTextField();
    }

    private int GetNewSeed()
    {
        return (int)(UnityEngine.Random.value * 100000);
    }

    private void NewSeed()
    {
        seed = GetNewSeed();
        UpdateTextField();
    }

    private void UpdateTextField()
    {
        string text = size + "." + seed;
        levelInput.text = text;
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetInt(SIZE_KEY, size);
        PlayerPrefs.SetInt(SEED_KEY, seed);
        PlayerPrefs.Save();
    }

    private void StartGame()
    {
        SavePrefs();

        SceneManager.LoadScene(1);
    }

    private void OpenHighScores()
    {
        print("Highscores");
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

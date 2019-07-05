using System;
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

    private int size;
    private int seed;

    void Start()
    {
        levelInput.onValueChanged.AddListener(InputChanged);

        size = PlayerPrefs.GetInt(SIZE_KEY, MEDIUM);
        seed = PlayerPrefs.GetInt(SEED_KEY, GetNewSeed());

        UpdateTextField();
    }

    public void EasySize()
    {
        SetSize(EASY);
    }

    public void MediumSize()
    {
        SetSize(MEDIUM);
    }

    public void HardSize()
    {
        SetSize(HARD);
    }

    public void NewSeed()
    {
        seed = GetNewSeed();
        UpdateTextField();
    }

    public void StartGame()
    {
        SavePrefs();

        SceneManager.LoadScene(1);
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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

    private void SetSize(int newSize)
    {
        size = newSize;
        UpdateTextField();
    }

    private int GetNewSeed()
    {
        return (int)(UnityEngine.Random.value * 100000);
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
}

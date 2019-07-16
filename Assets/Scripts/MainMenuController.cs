using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Main menu controller. Method calls from buttons connected in Unity.
 */
public class MainMenuController : MonoBehaviour
{
    public const string SIZE_KEY = "SIZE_KEY";
    public const string SEED_KEY = "SEED_KEY";

    public int smallSize = 20;
    public int mediumSize = 40;
    public int largeSize = 60;

    public int minSize = 5;
    public int maxSize = 1000;

    public InputField levelInput;

    private int size;
    private int seed;

    void Start()
    {
        // Set this listener up in code since we already have the reference.
        levelInput.onValueChanged.AddListener(InputChanged);

        size = PlayerPrefs.GetInt(SIZE_KEY, mediumSize);
        seed = PlayerPrefs.GetInt(SEED_KEY, GetNewSeed());

        UpdateTextField();
    }

    public void SmallSize()
    {
        SetSize(smallSize);
    }

    public void MediumSize()
    {
        SetSize(mediumSize);
    }

    public void LargeSize()
    {
        SetSize(largeSize);
    }

    public void NewSeed()
    {
        seed = GetNewSeed();
        UpdateTextField();
    }

    public void StartGame()
    {
        if (levelInput.text.Equals("")) {
            return;
        }

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

            if (size < minSize || size > maxSize) {
                size = mediumSize;
            }
        } catch (Exception) {
            size = mediumSize;
        }

        if (args.GetLength(0) < 2) {
            return;
        }

        try {
            seed = Convert.ToInt32(args[1]);

        } catch (Exception) {
            seed = GetNewSeed();
        }

        UpdateTextField();
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour
{
    public static string HIGHSCORE_FILENAME = "highscores.dat";

    public GameObject highscoreList;
    public GameObject levelSelector;
    public GameObject noHighScores;

    private HighscoreListController listController;
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, HIGHSCORE_FILENAME);
        listController = highscoreList.GetComponent<HighscoreListController>();

        if (File.Exists(filePath)) {
            levelSelector.SetActive(true);
        } else {
            noHighScores.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeLevel(int level)
    {
        listController.SetLevel(level.ToString());
        listController.Load();
        highscoreList.SetActive(true);
    }
}

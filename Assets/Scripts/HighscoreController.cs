using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour
{
    public GameObject highscoreList;
    public GameObject levelSelector;
    public GameObject noHighScores;

    private HighscoreListController listController;
    private string filePath;

    private void Awake()
    {
        listController = highscoreList.GetComponent<HighscoreListController>();

        HighscoreDataManager hsm = new HighscoreDataManager();
        if (hsm.HighscoresExist()) {
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

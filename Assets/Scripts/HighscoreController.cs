using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    public GameObject bestScoreText;
    public GameObject levelSelector;
    public GameObject noHighScores;

    private List<Highscore> highscores;

    private void Awake()
    {
        HighscoreDataManager hsm = new HighscoreDataManager();
        if (hsm.HighscoresExist()) {
            levelSelector.SetActive(true);
            highscores = hsm.LoadAll();
            PopulateLevelSelector();
            SetLevel(0);
        } else {
            noHighScores.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetLevel(int level)
    {
        Highscore hs = highscores[level];
        bestScoreText.GetComponent<Text>().text = "Score: " + Utils.SecondsToString(hs.Seconds) + " seconds\nDate: " + hs.Date;
        bestScoreText.SetActive(true);
    }

    private void PopulateLevelSelector()
    {
        Dropdown level = levelSelector.transform.GetChild(1).GetComponent<Dropdown>();
        level.AddOptions(highscores.Select((hs) => hs.Level).ToList());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Controller to show level highscores. Fetches the list of levels and allows the user to browse.
 */
public class HighscoreController : MonoBehaviour
{
    public Text bestScoreText;
    public GameObject levelSelector;
    public GameObject noHighScores;
    public GameObject playButton;

    private List<Highscore> highscores;
    private Highscore selectedHighscore;

    private void Start()
    {
        HighscoreDataManager hsm = new HighscoreDataManager();
        if (hsm.HighscoresExist()) {
            levelSelector.SetActive(true);
            playButton.SetActive(true);
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
        selectedHighscore = highscores[level];
        bestScoreText.text = "Score: " + Utils.SecondsToString(selectedHighscore.Seconds) + " seconds\nDate: " + selectedHighscore.Date;
        bestScoreText.gameObject.SetActive(true);
    }

    public void PlayLevel()
    {
        string[] level = selectedHighscore.Level.Split('.');
        int size = Convert.ToInt32(level[0]);
        int seed = Convert.ToInt32(level[1]);

        PlayerPrefs.SetInt(MainMenuController.SIZE_KEY, size);
        PlayerPrefs.SetInt(MainMenuController.SEED_KEY, seed);

        SceneManager.LoadScene(1);
    }

    private void PopulateLevelSelector()
    {
        Dropdown level = levelSelector.transform.GetChild(1).GetComponent<Dropdown>();

        // Map over highscores and select only the level names.
        level.AddOptions(highscores.Select((hs) => hs.Level).ToList());
    }
}

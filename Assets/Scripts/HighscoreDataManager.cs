using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/*
 * Abstraction layer over data file on the file system.
 */
public class HighscoreDataManager
{
    private const string FILENAME = "highscore.dat";

    private readonly string filePath;

    public HighscoreDataManager()
    {
        filePath = Path.Combine(Application.persistentDataPath, FILENAME);
    }

    /// <summary>
    /// True if any previous highscores exist
    /// </summary>
    public bool HighscoresExist()
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// Save highscore. Will overwrite previous if it exists.
    /// </summary>
    /// <param name="level">Level identifier (<c>size.seed</c>).</param>
    /// <param name="seconds">Highscore time</param>
    public void Save(string level, int seconds)
    {
        Highscore highscore = new Highscore(level, seconds);

        List<Highscore> list = LoadAll();
        int index = list.FindIndex((hs) => hs.Level == level);

        if (index > -1) {
            list[index] = highscore;
        } else {
            list.Add(highscore);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.OpenOrCreate);

        bf.Serialize(file, list);
        file.Close();
    }

    /// <summary>
    /// Get the highscore for <c>level</c>.
    /// </summary>
    /// <param name="level">Level identifier (<c>size.seed</c>).</param>
    /// <returns>Highscore or null.</returns>
    public Highscore GetScoreForLevel(string level)
    {
        try {
            return LoadAll().First((hs) => hs.Level == level);
        } catch (Exception) {
            return null;
        }
    }

    /// <summary>
    /// Load all highscores
    /// </summary>
    /// <returns>List of highscores.</returns>
    public List<Highscore> LoadAll()
    {
        if (!HighscoresExist()) {
            return new List<Highscore>();
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);
        List<Highscore> list = (List<Highscore>)bf.Deserialize(file);

        file.Close();

        return list;
    }
}

[Serializable]
public class Highscore
{
    public int Seconds { get; }
    public string Level { get; }
    public DateTime Date { get; }

    public Highscore(string level, int seconds)
    {
        this.Level = level;
        this.Seconds = seconds;

        this.Date = DateTime.Now;
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighscoreDataManager
{
    private const string FILENAME = "highscore.dat";

    private string filePath;

    public HighscoreDataManager()
    {
        filePath = Path.Combine(Application.persistentDataPath, FILENAME);
    }

    public bool HighscoresExist()
    {
        return File.Exists(filePath);
    }

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

    public List<Highscore> GetLevels()
    {
        if (!HighscoresExist()) {
            return new List<Highscore>();
        }

        return new List<Highscore>();
    }

    public Highscore GetScoreForLevel(string level)
    {
        try {
            return LoadAll().First((hs) => hs.Level == level);
        } catch (Exception) {
            return null;
        }
    }

    private List<Highscore> LoadAll()
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
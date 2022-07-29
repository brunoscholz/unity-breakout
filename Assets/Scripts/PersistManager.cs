using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public int score;
}

public class PersistManager : MonoBehaviour
{
    // ENCAPSULATION
    public static PersistManager Instance { get; private set; }

    public List<SaveData> scores;
    public string PlayerName;
    public int HighScore;
    public string currentPlayer;
    public int currentLevel = 1;

    public void SaveScore(string m_playerName, int m_highScore)
    {
        SaveData newData = new SaveData();
        newData.playerName = m_playerName;
        newData.score = m_highScore;

        scores.Add(newData);
        scores.Sort(SortByScore);

        SaveData[] data = scores.ToArray();
        PlayerName = data[0].playerName;
        HighScore = data[0].score;

        // string json = JsonUtility.ToJson(data);
        string json = JsonHelper.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            // SaveData data = JsonUtility.FromJson<SaveData>(json);
            SaveData[] data = JsonHelper.FromJson<SaveData>(json);

            if (data.Length > 0) {
              scores = new List<SaveData>(data);
              PlayerName = data[0].playerName;
              HighScore = data[0].score;
            } else {
                PlayerName = "";
                HighScore = 0;
            }
        }
    }

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    static int SortByScore(SaveData p1, SaveData p2)
    {
        return p2.score.CompareTo(p1.score);
    }
}

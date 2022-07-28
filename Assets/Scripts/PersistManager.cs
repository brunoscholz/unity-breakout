using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistManager : MonoBehaviour
{
    public static PersistManager Instance;

    public string PlayerName;
    public string HighScore;

    [System.Serializable]
    class SaveData
    {
        public string PlayerName;
        public string HighScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.HighScore = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
            HighScore = data.HighScore;
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
}

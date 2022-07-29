using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHighScore : MonoBehaviour
{
    public Transform list;
    public Transform line;

    void Start()
    {
        if (PersistManager.Instance != null) {
            foreach (SaveData item in PersistManager.Instance.scores)
            {
                Transform newLine = Instantiate(line, list);
                Text name = newLine.Find("Name").GetComponent<Text>();
                Text score = newLine.Find("Score").GetComponent<Text>();
                name.text = item.playerName;
                score.text = $"{item.score}";
            }
        }
    }

    public void Exit() {
        SceneManager.LoadScene(0);
    }
}

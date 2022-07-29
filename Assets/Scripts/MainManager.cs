using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public Text LevelText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private string PlayerName;
    private int HighScore;
    private string currentPlayer = "";
    private int currentLevel = 1;

    private BrickFactory factory = new BrickFactory();

    // Start is called before the first frame update
    void Start()
    {
        if (PersistManager.Instance != null)
        {
            currentPlayer = PersistManager.Instance.currentPlayer;
            PlayerName = PersistManager.Instance.PlayerName;
            HighScore = PersistManager.Instance.HighScore;
            SetHighScore();
            m_Points = 0;
            AddPoint(PersistManager.Instance.currentScore);
            currentLevel = PersistManager.Instance.currentLevel;
            LevelText.text = $"Level {currentLevel}";
            // ABSTRACTION
            LoadLevel();
        }
    }

    string[][] readFile(string file) {
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\n");
        int rows = lines.Length;

        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)  {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }

    void LoadLevel() {
        string path = "./Assets/Levels/";
        string filename = $"level{currentLevel}.txt";
        string[][] newLevel = readFile(path+filename);
        const float step = 0.6f;
        int rows = newLevel.Length;
        int cols = newLevel[0].Length;

        for (int y = 0; y < rows; ++y) {
            for (int x = 0; x < cols; ++x) {
                // Debug.Log(newLevel[y][x]);
                if (newLevel[y][x] == "X") {
                    continue;
                }

                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + (rows - y - 1) * 0.3f, 0);
                GameObject go = Instantiate(BrickPrefab, position, Quaternion.identity);
                // INHERITANCE with factory of bricks
                BaseBrick brick = factory.GetBrick(newLevel[y][x], go);
                brick.onDestroyed += (hit) => AddPoint(hit);
            }
        }
    }

    // ABSTRACTION
    private void KickOff()
    {
        m_Started = true;
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                KickOff();
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void SetHighScore() {
        HighScoreText.text = $"Best Score : {PlayerName} : {HighScore}";
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (currentPlayer != "") {
            ScoreText.text = $"{currentPlayer}'s {ScoreText.text}";
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        bool m_Finished = false;

        // GameObject.FindAll bricks if null or 0 next, game over
        GameObject[] allBricks = GameObject.FindGameObjectsWithTag("Brick");
        m_Finished = allBricks.Length == 0 ? true : false;

        currentLevel++;
        if (currentLevel > 2 || !m_Finished) {
            PersistManager.Instance.currentLevel = 1;
            currentLevel = 1;
            GameOverText.GetComponent<Text>().text = "GAME OVER\nPress Space to Restart";

            if (m_Points >= HighScore) {
                PlayerName = currentPlayer;
                HighScore = m_Points;
            }

            PersistManager.Instance.currentScore = 0;
            PersistManager.Instance.SaveScore(currentPlayer, m_Points);
            SetHighScore();

        } else {
            PersistManager.Instance.currentLevel++;
            PersistManager.Instance.currentScore = m_Points;
            GameOverText.GetComponent<Text>().text = $"Level{currentLevel} Cleared!\nPress Space for next";
        }

        GameOverText.SetActive(true);
    }

    public void Exit() {
        SceneManager.LoadScene(0);
    }
}

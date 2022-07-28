using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class UIMainMenu : MonoBehaviour
{
    public Button startButton;
    public InputField inputName;
    public Text warningText;
    private bool hasName = false;

    void Start() {
        inputName.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        // Debug.Log("Value Changed: " + inputName.text.Length );
        if (inputName.text.Length >= 3) {
            hasName = true;
        } else {
            hasName = false;
        }
    }

    public void Play() {
        if (hasName) {
            SceneManager.LoadScene(1);
        } else {
            warningText.gameObject.SetActive(true);
        }
    }

    public void Exit() {
        // MainManager.Instance.SaveScore();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

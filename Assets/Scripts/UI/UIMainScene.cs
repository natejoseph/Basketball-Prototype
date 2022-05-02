using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMainScene : MonoBehaviour
{
    public TextMeshProUGUI bestScoreOutput;
    Counter counterScript;

    // Start is called before the first frame update
    void Start()
    {
        counterScript = GameObject.Find("Hoop").GetComponent<Counter>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counterScript.Count > MainManager.Instance.score)
        {
            MainManager.Instance.userName = MainManager.Instance.inputName;
            MainManager.Instance.score = counterScript.Count;
            MainManager.Instance.SaveHighScore();
        }
        bestScoreOutput.text = "Best Score: " + MainManager.Instance.userName + ": " + MainManager.Instance.score;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

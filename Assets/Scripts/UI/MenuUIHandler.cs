using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{

    public TMP_InputField nameInput;
    public string inputName = "Anonymous";
    public TextMeshProUGUI bestScoreOutput;

    private void Start()
    {
        nameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        bestScoreOutput.text = "Best Score: " + MainManager.Instance.userName + ": " + MainManager.Instance.score;
    }

    public void StartNew()
    {
        MainManager.Instance.inputName = inputName;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void InputName()
    {
        inputName = nameInput.text;
        nameInput.text = "";
    }
}
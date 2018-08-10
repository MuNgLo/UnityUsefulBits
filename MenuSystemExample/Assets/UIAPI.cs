using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAPI : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
       SceneManager.LoadSceneAsync(levelName);
    }
}

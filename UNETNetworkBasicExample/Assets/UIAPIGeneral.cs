using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAPIGeneral : MonoBehaviour {
    public MenuSystem menus;
	public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        menus.GotToMenu("Chat");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

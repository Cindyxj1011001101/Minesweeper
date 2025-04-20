using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteGameButton : MonoBehaviour
{
    public GameObject PauseMenu;
    public void EnterScene(string sceneName)
    {
        FindObjectOfType<SceneFader>().FadeToScene(sceneName);
    }

    public void EnterPauseScene()
    {
        PauseMenu.SetActive(true);
    }
    public void LeavePauseScene()
    {
        PauseMenu.SetActive(false);
    }

    public void RefreshScene()
    {
        FindObjectOfType<SceneFader>().FadeToScene(SceneManager.GetActiveScene().name);
    }
}
    
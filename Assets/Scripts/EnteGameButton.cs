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
        GlobalData.Instance.isDialogueMode = true;
        PauseMenu.SetActive(true);
    }
    public void LeavePauseScene()
    {
        GlobalData.Instance.isDialogueMode = false;
        PauseMenu.SetActive(false);
    }

    public void RefreshScene()
    {
        FindObjectOfType<SceneFader>().FadeToScene(SceneManager.GetActiveScene().name);
    }
}
    
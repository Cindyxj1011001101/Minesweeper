using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteGameButton : MonoBehaviour
{
    public void EnterScene(string sceneName)
    {
        FindObjectOfType<SceneFader>().FadeToScene(sceneName);
    }
}

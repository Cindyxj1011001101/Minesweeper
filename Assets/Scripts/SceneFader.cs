using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    private float alpha;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            this.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }   

    IEnumerator FadeOut(string sceneName)
    {
        alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            this.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public Slider loadingBar;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation =
            SceneManager.LoadSceneAsync("MainMenu");

        operation.allowSceneActivation = false;

        float timer = 0;

        while (timer < 3f)
        {
            timer += Time.deltaTime;

            loadingBar.value = timer / 3f;

            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}
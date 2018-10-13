using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    AsyncOperation asyncOperation;
    [SerializeField] GameObject loadingUI;
    [SerializeField] GameObject uiToHide;
    [SerializeField] Image loadingProgbar;
    

    // the actual percentage while scene is fully loaded

    public void ChangeScene(string sceneName)
    {
        uiToHide.SetActive(false);
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // disable scene activation while loading to prevent auto load
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            loadingProgbar.fillAmount = asyncOperation.progress;

            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject loadingimage;
    [SerializeField] Slider loadingBar;
    [SerializeField] Image Shade;

    private BaseScene curScene;
    public BaseScene GetCurScene()
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }

        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if (curScene == null)
        {
            curScene = FindObjectOfType<BaseScene>();
        }

        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string sceneName)
    {
        canvas.gameObject.SetActive(true);
        // Fade In
        Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, 0f);
        for (float t = 0f; t < 1f; t += Time.deltaTime / 2)
        {
            float alpha = Mathf.Lerp(0f, 1f, t);
            Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, alpha);
            yield return null;
        }

        loadingimage.gameObject.SetActive(true);
        BaseScene prevScene = GetCurScene();
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        oper.allowSceneActivation = true;
        while (oper.isDone == false)
        {
            Debug.Log(oper.progress);
            loadingBar.value = Mathf.Lerp(0f, 0.2f, oper.progress);
            yield return null;
        }

        Shade.gameObject.SetActive(true);
        Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, 1f);
        BaseScene curScene = GetCurScene();
        yield return curScene.LoadingRoutine();
        loadingBar.value = 1f;
        loadingimage.gameObject.SetActive(false);


        // Fade out
        for (float t = 1f; t > 0f; t -= Time.deltaTime / 1)
        {
            float alpha = Mathf.Lerp(0f, 1f, t);
            Shade.color = new Color(Shade.color.r, Shade.color.g, Shade.color.b, alpha);
            yield return null;
        }
        canvas.gameObject.SetActive(false);
    }

    private void Upadate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GetCurScene().gameObject.name);
        }
    }

    public int GetCurSceneIndex()
    {
        return UnitySceneManager.GetActiveScene().buildIndex;
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void SetLoadingBarValue(float value)
    {
        loadingBar.value = value;
    }
}

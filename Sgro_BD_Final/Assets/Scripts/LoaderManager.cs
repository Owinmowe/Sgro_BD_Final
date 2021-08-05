using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionData
{
    public int score = 0;
    public int deaths = 0;
    public string username = "Name";
}

public class LoaderManager : MonoBehaviour
{

    [SerializeField] float gameStartTime = 1f;
    [SerializeField] float minTimeToLoadScene = 1f;
    [SerializeField] UI_LoadingScreen uI_LoadingScreen = null;

    private static LoaderManager instance = null;

    SessionData currentSessionData = new SessionData();

    static public LoaderManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(MainMenuCoroutine());
    }

    IEnumerator MainMenuCoroutine()
    {
        yield return new WaitForSeconds(gameStartTime);
        uI_LoadingScreen.FadeWithBlackScreen();
        uI_LoadingScreen.UnlockFade();
    }

    public void SetSessionData(string username, int score, int deaths)
    {
        currentSessionData.username = username;
        currentSessionData.score = score;
        currentSessionData.deaths = deaths;
    }

    public SessionData GetSessionData()
    {
        return currentSessionData;
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(AsynchronousLoadWithFake(sceneName));
    }

    IEnumerator AsynchronousLoadWithFake(string scene)
    {
        float loadingProgress = 0;
        float timeLoading = 0;

        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        uI_LoadingScreen.FadeWithBlackScreen();
        uI_LoadingScreen.LockFade();
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            timeLoading += Time.deltaTime;
            loadingProgress = ao.progress + 0.1f;
            loadingProgress = loadingProgress * timeLoading / minTimeToLoadScene;

            // Se completo la carga
            if (loadingProgress >= 1)
            {
                ao.allowSceneActivation = true;
                uI_LoadingScreen.UnlockFade();
            }
            yield return null;
        }

    }

    public void FakeLoad(float time)
    {
        StartCoroutine(FakeLoadingWithBlackScreen(time));
    }

    public void FakeLoad(float time, string text)
    {
        StartCoroutine(FakeLoadingWithBlackScreen(time, text));
    }

    IEnumerator FakeLoadingWithBlackScreen(float time)
    {
        uI_LoadingScreen.FadeWithBlackScreen();
        uI_LoadingScreen.LockFade();
        yield return new WaitForSeconds(time);
        uI_LoadingScreen.UnlockFade();
    }

    IEnumerator FakeLoadingWithBlackScreen(float time, string text)
    {
        uI_LoadingScreen.FadeWithBlackScreen(text);
        uI_LoadingScreen.LockFade();
        yield return new WaitForSeconds(time);
        uI_LoadingScreen.UnlockFade();
    }

}

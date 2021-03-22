using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private string sceneToLoad;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (Instance != this)
            Destroy(gameObject);
    }

    public void LoadLevel(string level)
    {
        sceneToLoad = level;
        StartCoroutine(load());
    }    

    IEnumerator load()
    {
        Time.timeScale = 1.0f;

        AsyncOperation loading = SceneManager.LoadSceneAsync("Loading");

        while(!loading.isDone)
        {
            //Do Something Here
            yield return null;
        }
        
        loading = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loading.isDone)
        {
            //Do Something Here
            yield return null;
        }
    }
}

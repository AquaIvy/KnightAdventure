using Aquaivy.Core.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool autoLoad = false;
    public string loadSceneName = string.Empty;
    public float loadDelayTime = 1.5f;

    void Start()
    {
        if (autoLoad)
        {
            Invoke("LoadScene", 1.5f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Single);
    }

    public static void LoadSceneBySetting()
    {
        var go = GameObject.Find("/SceneLoader");
        if (go == null)
        {
            Log.Error("Not found [SceneLoader]");
            return;
        }

        var loader = go.GetComponent<SceneLoader>();
        loader.LoadScene();
    }
}

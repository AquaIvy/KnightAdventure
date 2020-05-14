using Aquaivy.Core.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool autoSkip = false;
    public string skipSceneName = string.Empty;

    void Start()
    {
        if (autoSkip)
        {
            Invoke("LoadScene", 1.5f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(skipSceneName, LoadSceneMode.Single);
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

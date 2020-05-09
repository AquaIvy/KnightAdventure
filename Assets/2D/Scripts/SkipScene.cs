using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public bool autoSkip = false;
    public string skipSceneName = string.Empty;

    void Start()
    {
        if (autoSkip)
        {
            Invoke("Skip", 1.5f);
        }
    }

    private void AutoSkip()
    {

    }

    void Update()
    {

    }

    public void Skip()
    {
        SceneManager.LoadSceneAsync(skipSceneName, LoadSceneMode.Single);
    }
}

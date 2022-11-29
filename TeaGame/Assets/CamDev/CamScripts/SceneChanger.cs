using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(12f);
        SceneManager.LoadScene("CamDev_TazBlockout");
    }

    public void Start()
    {
        StartCoroutine(WaitToLoad());
    }
}

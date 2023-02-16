using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private CycleManager _cycleManager;

    public void Start()
    {
        _cycleManager = GameObject.Find("CycleManager").GetComponent<CycleManager>();
    }

    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        SceneManager.LoadScene(_cycleManager.sceneIndex);
        _cycleManager.sceneIndex++;
        _cycleManager.cycleCount++;
    }
}

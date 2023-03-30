using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class QuitButton : MonoBehaviour
{
    private GameObject _cycleObj;
    [SerializeField] private CycleManager cycleManager;

    private void Awake()
    {
        _cycleObj = GameObject.Find("CycleManager");
        cycleManager = CycleManager.FindInstance();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Destroy(_cycleObj);
    }
}

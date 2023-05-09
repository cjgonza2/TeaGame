using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    private CycleManager _cycleManager;
    private GameManager _gameManager;

    public void Start()
    {
        _cycleManager = GameObject.Find("CycleManager").GetComponent<CycleManager>();
    }

    public void Update()
    {
        if (_cycleManager.gameIsPaused) return;

        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (SceneManager.GetActiveScene().name == "Epilogue")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            _cycleManager.sceneIndex++;
            SceneManager.LoadScene(_cycleManager.sceneIndex);
            //_cycleManager.cycleCount++;
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CycleManager : MonoBehaviour
{
    
    #region Singleton
    private static CycleManager _instance;
    public static CycleManager FindInstance()
    {
        return _instance;
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else 
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Debug.Log("Current Loop: " + cycleCount);
        Debug.Log("Current Index: " + sceneIndex);
    }
    #endregion

    public bool gameIsPaused;
    
    [HideInInspector]
    public int cycleCount;

    //Tells game managers/scene changers which scene to load in a given context.
    public int sceneIndex;

    private void Start()
    {
        Debug.Log("Current Loop: " + cycleCount);
        Debug.Log("Current Index: " + sceneIndex);
    }

    private void Update()
    {
        PauseInput();
        /*PauseGame();
        UnPauseGame();*/
    }

    private void PauseInput()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return; //if there is no escape input return.
        
        if (!gameIsPaused) //if the game is not paused;
        {
            gameIsPaused = true;
        } else if (gameIsPaused)
        {
            gameIsPaused = false;
        }

        //Debug.Log(gameIsPaused);
    }

    public void NextScene()
    {
        sceneIndex++;
        if (sceneIndex >= 6)
        {
            sceneIndex = 1;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}

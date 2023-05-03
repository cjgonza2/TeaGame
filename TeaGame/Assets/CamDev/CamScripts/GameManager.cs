using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ChracterManager charMan;
    [SerializeField] private CycleManager cycleManager;
   
    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject quitButton;
    public bool gamePaused;

    [Header("Current Scene Info")]
    public string _currentScene;

    [Header("Pouring Check")]
    public bool finishedPouring;

    [Header("Inventory Counts")] 
    [SerializeField] private List<GameObject> ingPots = new List<GameObject>();

    [Header("Compendium Check")]
    [SerializeField] private TeaCompendiumController teaCompendium;
    public bool compendiumOpen;
    
    [Header("Current Game State")]
    public State currentState;

    public enum State
    {
        Enter,
        Resting,
        Drinking,
        Tasting,
        Exiting
    }

    #region CoRoutines

    private IEnumerator WaitBeforeTaste()
    {
        yield return new WaitForSeconds(5f);
        TransitionState(State.Tasting);
    }

    IEnumerator WaitBeforeExit()
    {
        yield return new WaitForSeconds(3f);
        TransitionState(State.Exiting);
    }

    IEnumerator Goyo_CanyonIngredientLoad()
    {
        switch (cycleManager.cycleCount)
        {
            case 1:
                ingPots[0].SetActive(true);
                ingPots[3].SetActive(true);
                break;
            case >1:
                DefaultLoad();
                break;
        }
        yield break;
    }

    IEnumerator Lily_BogIngredientLoad()
    {
        switch (cycleManager.cycleCount)
        {
            case 1:
                ingPots[0].SetActive(true);
                ingPots[1].SetActive(true);
                ingPots[3].SetActive(true);
                break;
            case >1:
                DefaultLoad();
                break;
        }
        yield break;
    }

    IEnumerator Ootal_CliffsIngredientLoad()
    {
        switch (cycleManager.cycleCount)
        {
            case 1:
                ingPots[0].SetActive(true);
                ingPots[1].SetActive(true);
                ingPots[3].SetActive(true);
                ingPots[5].SetActive(true);
                break;
            case >1:
                DefaultLoad();
                break;
        }
        yield break;
    }
    #endregion

    private void DefaultLoad()
    {
        foreach (var pot in ingPots)
        {
            pot.SetActive(true);
        }
    }
    
    private void Awake()
    {
        cycleManager = CycleManager.FindInstance();
    }

    void Start()
    {
        cycleManager = CycleManager.FindInstance();
        SetScene();
        StartCoroutine($"{_currentScene}IngredientLoad");
        TransitionState(State.Enter);
    }

    IEnumerator WaitBeforeTransition()
    {
        yield return new WaitForSeconds(3f);
        NextLocation();
        
    }

    private void Update()
    {
        gamePaused = cycleManager.gameIsPaused;
        compendiumOpen = teaCompendium.isOpen;
        
        PauseGame();
        UnPauseGame();

        SetScene(); //sets the current scene for reference purposes.

        if (charMan._sceneEnd)
        {
            StartCoroutine(WaitBeforeTransition());
        }
    }

    private void PauseGame()
    {
        if (!gamePaused) return; //if the game is not paused, returns.
        pausePanel.SetActive(true);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        gamePaused = true;
        //Time.timeScale = 0f;
    }

    private void UnPauseGame()
    {
        if (gamePaused) return; //if game is paused, returns
        pausePanel.SetActive(false);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        gamePaused = false;
        //Time.timeScale = 1f;
    }
    
    private void SetScene()
    {
        _currentScene = SceneManager.GetActiveScene().name;
    }
    
    
    public void TransitionState(State newState)
    {
        currentState = newState;
        switch (newState)
        {
            case State.Enter:
                //StartCoroutine(WaitBeforeRest());
                //(State.Resting);
                break;
            case State.Resting:
                break;
            case State.Drinking:
                StartCoroutine(WaitBeforeTaste());
                break;
            case State.Tasting:
                StartCoroutine(WaitBeforeExit());
                break;
            case State.Exiting:
                NextLocation();
                break;
        }
    }

    private void NextLocation()
    {
        finishedPouring = false;
        /*
        Debug.Log("I am in the process of changing scenes");
        cycleManager.sceneIndex++;
        if (cycleManager.sceneIndex > 5)
        {
            cycleManager.sceneIndex = 2;
            cycleManager.cycleCount++;
        }

        StartCoroutine(WaitForCharExit());
        */

        /*StartCoroutine(WaitForCharExit());
        SceneManager.LoadScene(cycleManager.sceneIndex);
        TransitionState(State.Enter);*/
    }

    private IEnumerator WaitForCharExit()
    {
        while (!charMan._sceneEnd) //while scene end bool is falls, it will return.
        {
            yield return null;
        }
        //once its true it will load the next scene and changes the state.
        SceneManager.LoadScene(cycleManager.sceneIndex);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ChracterManager charMan;
    [SerializeField] private CycleManager cycleManager;

    public bool finishedPouring;
    public bool lidMoved;

    /*#region Singleton
    private static GameManager _instance;

    public static GameManager FindInstance()
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
        Debug.Log("Awake was called.");
        _currentScene = SceneManager.GetActiveScene().name;
    }
    #endregion*/

    //[HideInInspector]
    public string _currentScene;

    public int hakaCount;
    public int tallowCount;
    public int bombomCount;
    public int aileCount;
    public int shnootCount;
    public int poffCount;

    public enum State
    {
        Enter,
        Resting,
        Drinking,
        Tasting,
        Exiting
    }

    public State currentState;

    #region CoRoutines
    /*private IEnumerator WaitBeforeRest()
    {
        yield return new WaitForSeconds(1.0f);
    }*/
    
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
    #endregion
    
    void Start()
    {
        //lcycleManager = GameObject.Find("CycleManager").GetComponent<CycleManager>();
        //_currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(_currentScene);
        SetScene();
        cycleManager = CycleManager.FindInstance();
        TransitionState(State.Enter);
    }

    IEnumerator WaitBeforeTransition()
    {
        yield return new WaitForSeconds(3f);
        NextLocation();
        
    }

    private void Update()
    {
        /*//checks if scene transitioning works.
        if (Input.GetKey(KeyCode.Space))
        {
            NextLocation();
        }*/

        SetScene(); //sets the current scene for reference purposes.

        if (charMan._sceneEnd)
        {
            StartCoroutine(WaitBeforeTransition());
        }
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
        Debug.Log("I am in the process of changing scenes");
        cycleManager.sceneIndex++;
        if (cycleManager.sceneIndex > 5)
        {
            cycleManager.sceneIndex = 2;
            cycleManager.cycleCount++;
        }

        StartCoroutine(WaitForCharExit());

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

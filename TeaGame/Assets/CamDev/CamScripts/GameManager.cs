using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private ChracterManager charMan;

    [SerializeField] 
    private CycleManager cyclMan;

    #region Singleton
    private static GameManager instance;

    public static GameManager FindInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    //[HideInInspector]
    public string _currentScene;
    
    public enum State
    {
        Enter,
        Resting,
        Drinking,
        Tasting,
        Exiting
    }

    public State currentState;
    // Start is called before the first frame update

    private IEnumerator WaitBeforeRest()
    {
        yield return new WaitForSeconds(1.0f);
    }
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
    void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(_currentScene);
        TransitionState(State.Enter);
    }

    IEnumerator WaitBeforeTransition()
    {
        yield return new WaitForSeconds(3f);
        cyclMan.cycleCount++;
        NextCycle();
        
    }

    private void Update()
    {
        if (charMan._sceneEnd)
        {
            StartCoroutine(WaitBeforeTransition());
        }
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
                break;
        }
    }

    private void NextCycle()
    {
        Debug.Log("I am in the process of changing scenes");
        SceneManager.LoadScene(0);
    }
}

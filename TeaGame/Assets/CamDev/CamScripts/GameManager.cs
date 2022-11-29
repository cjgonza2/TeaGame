using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private ChracterManager charMan;

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
        TransitionState(State.Enter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransitionState(State newState)
    {
        currentState = newState;
        switch (newState)
        {
            case State.Enter:
                TransitionState(State.Resting);
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
}

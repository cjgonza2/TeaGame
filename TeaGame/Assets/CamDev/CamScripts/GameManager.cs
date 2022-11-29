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
                break;
            case State.Resting:
                break;
            case State.Drinking:
                break;
            case State.Tasting:
                break;
            case State.Exiting:
                break;
        }
    }
}

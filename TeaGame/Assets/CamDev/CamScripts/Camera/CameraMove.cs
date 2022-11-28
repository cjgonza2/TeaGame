using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Animator camAnimator;

    public enum State
    {
        Default,
        LookUp,
        LookDown,
    }

    public State CurrentState;

    // Update is called once per frame
    void Update()
    {
        Lookup();
    }

    void Lookup()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TransitionState(State.LookUp);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (CurrentState == State.LookUp)
            {
                TransitionState(State.LookDown);
            }
        }
    }

    void TransitionState(State newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case State.Default:
                break;
            case State.LookUp:
                camAnimator.Play("MoveUp", 0 ,0f);
                break;
            case State.LookDown:
                camAnimator.Play("MoveDown", 0, 0f);
                TransitionState(State.Default);
                break;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PouringManager : MonoBehaviour
{

    public Animator potAnimator;
    public Animator liqAnimator;

    [SerializeField]
    private GameObject teaPot;
    [SerializeField]
    private Rigidbody2D teaPot_RB;
    [SerializeField]
    private GameObject teaLiquid;

    private bool animationDone; //whether animation is done or not
    private bool canSwitch = true; //whether a state can switch or not.
    public bool changeSprite;

    #region singleton
    private static PouringManager instance;

    public static PouringManager FindInstance()
    {
        return instance;
    }

    private void Awake()
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

    #region StateDecleration

    public enum State
    {
        Resting,
        Pouring,
        Reset
    }

    public State CurrentState;
    #endregion

    #region Coroutines

    IEnumerator AnimationDone()
    {
        teaPot_RB.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSecondsRealtime(potAnimator.GetCurrentAnimatorClipInfo(0).Length);
        animationDone = true;
        changeSprite = true;
        teaPot_RB.constraints = RigidbodyConstraints2D.None;
    }

    //coroutine that gives a buffer between each state switch.
    IEnumerator StateBuffer()
    {
        canSwitch = false;
        yield return new WaitForSeconds(0.1f);
        canSwitch = true;
    }

    #endregion

    #region StateMachine

    public void TransitionState(State newState)
    {
        if (canSwitch)
        {
            CurrentState = newState; //sets current state to state declared when function is called.
            switch (newState)
            {
                case State.Resting:
                    break;
                case State.Pouring:
                    potAnimator.Play("TeaPot Body", 0, 0f); //plays pouring animation.
                    liqAnimator.Play("pour_animation", 0, 0f); //plays the liquid pouring.
                    StartCoroutine(AnimationDone()); //checks if animation is done
                    if (animationDone) //if so 
                    {
                        StartCoroutine(StateBuffer()); //implement the state buffer.
                        animationDone = false; //sets animationdone back to false. 
                    }
                    break;
                case State.Reset:
                    potAnimator.Play("ResetPot", 0, 0f); //plays reset animation
                    TransitionState(State.Resting); //sets state back to default
                    break;
            }
        }
    }

    #endregion
}

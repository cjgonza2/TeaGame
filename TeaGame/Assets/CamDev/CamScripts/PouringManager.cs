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
    private GameObject teaLiquid;

    private bool animationDone;
    private bool canSwitch = true;
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
        yield return new WaitForSecondsRealtime(potAnimator.GetCurrentAnimatorClipInfo(0).Length);
        animationDone = true;
        changeSprite = true;
    }

    IEnumerator StateBuffer()
    {
        canSwitch = false;
        yield return new WaitForSeconds(0.1f);
        canSwitch = true;
    }

    #endregion

    private void Start()
    {
        
    }

    #region StateMachine

    public void TransitionState(State newState)
    {
        if (canSwitch)
        {
            CurrentState = newState;
            switch (newState)
            {
                case State.Resting:
                    break;
                case State.Pouring:
                    potAnimator.Play("TeaPot Body", 0, 0f);
                    liqAnimator.Play("pour_animation", 0, 0f);
                    StartCoroutine(AnimationDone());
                    if (animationDone)
                    {
                        StartCoroutine(StateBuffer());
                        animationDone = false;
                    }
                    break;
                case State.Reset:
                    potAnimator.Play("ResetPot", 0, 0f);
                    TransitionState(State.Resting);
                    break;
            }
        }
    }

    #endregion
}

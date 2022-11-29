using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PouringManager : MonoBehaviour
{

    [Header("Animators")]
    [SerializeField]
    private Animator potAnimator;
    [SerializeField]
    private Animator liqAnimator;
    [SerializeField]
    private Animator ketAnimator;
    [SerializeField]
    private Animator KetLiqAnimator;

    [Header("SpriteTracking")] 
    [SerializeField]
    private Pot_SpriteChanger potSpr;
    
    
    [Header("GameObjects")]
    [SerializeField]
    private GameObject teaPot;
    [SerializeField]
    private Rigidbody2D teaPot_RB;
    [SerializeField]
    private GameObject teaLiquid;
    
    [Header("Bools")]
    public bool changeSprite;
    private bool animationDone; //whether animation is done or not
    private bool canSwitch = true; //whether a state can switch or not.

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
        Reset,
        KettlePour,
        KettleReset
    }

    public State CurrentState;
    #endregion

    #region Coroutines

    IEnumerator PotAnimationDone()
    {
        teaPot_RB.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSecondsRealtime(potAnimator.GetCurrentAnimatorClipInfo(0).Length);
        animationDone = true;
        changeSprite = true;
        teaPot_RB.constraints = RigidbodyConstraints2D.None;
    }

    IEnumerator KettleDone()
    {
        yield return new WaitForSeconds(ketAnimator.GetCurrentAnimatorClipInfo(0).Length);
        animationDone = true;
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
                    StartCoroutine(PotAnimationDone()); //checks if animation is done
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
                case State.KettlePour:
                    ketAnimator.Play("KettlePour", 0, 0f);
                    KetLiqAnimator.Play("water_pour", 0, 0f);
                    StartCoroutine(KettleDone());
                    if (animationDone)
                    {
                        StartCoroutine(StateBuffer());
                        animationDone = false;
                    }
                    break;
                case State.KettleReset:
                    ketAnimator.Play("KettleReset",0,0f);
                    TransitionState(State.Resting);
                    break;
            }
        }
    }

    #endregion
}

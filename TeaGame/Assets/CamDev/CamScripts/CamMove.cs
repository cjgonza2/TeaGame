using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    //do tween - an extension for tweening that might be worth looking into.
    public Animator animator; //references our animator.

    Vector3 mousePosOffset;
    private float mouseZ;

    private bool animationDone; //whether animation is done or not
    private bool canSwitch = true; //whether a state can switch or not.

    //Declares States
    public enum State
    {
        Default,
        Pouring,
        Reset
    }

    public State currentState;  //Variable state that is set to current state.

    //coroutine that returns if the
    IEnumerator AnimationDone()
    {
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0).Length); //waits for the length of the current animation before declaring animation bool to ture.
        animationDone = true;
    }
    
   //coroutine that gives a buffer between each state switch.
    IEnumerator StateBuffer()
    {
        canSwitch = false;
        yield return new WaitForSeconds(0.1f);
        canSwitch = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionState(State.Default); //sets state to default.
    }

    #region StateMachine
    private void TransitionState(State newstate)
    {
        if (canSwitch)
        {
            currentState = newstate; //sets current state to state declared when function is called.
            //state Machine
            switch (newstate)
            {
                case State.Default:
                    break;
                case State.Pouring:
                    
                    animator.Play("TeaPot Body", 0, 0f); //plays pouring animation
                    StartCoroutine(AnimationDone());
                    if (animationDone)
                    {
                        StartCoroutine(StateBuffer());
                        animationDone = false;
                    }
                    break;
                case State.Reset:
                    animator.Play("ResetPot", 0, 0f);
                    TransitionState(State.Default);
                    break;
            }
        }
    }
    #endregion

    #region Obj Movement
    private void OnMouseDown()
    {
        Debug.Log("click");
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();
    }
    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return world point
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        Debug.Log("drag");
        transform.position = GetMouseWorldPosition() + mousePosOffset;
    }
    #endregion


    #region Collisions
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cup"))
        {
            TransitionState(State.Pouring);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cup"))
        {
            TransitionState(State.Reset);
        }
    }
    #endregion
}

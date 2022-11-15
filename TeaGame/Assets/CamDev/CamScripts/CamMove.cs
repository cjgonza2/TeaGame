using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    //private Cam_PouringManager pouringManager;
    
    //do tween - an extension for tweening that might be worth looking into.
    /*public Animator animator; //references our animator.
     

    [SerializeField]
    private GameObject pouringAnim;

    [SerializeField]
    private Animator pourAnimator;*/
    
    [SerializeField]
    PouringManager _myManager;

    Vector3 mousePosOffset;
    private float mouseZ;

    [SerializeField]
    private Rigidbody2D teaPot;

    /*private bool animationDone; //whether animation is done or not
    private bool canSwitch = true; //whether a state can switch or not.
    public bool changeSprite;*/
    
    #region Singleton
    private static CamMove instance;
    public static CamMove FindInstance()
    {
        return instance; //that's just a singletone as the region says
    }

    void Awake() //this happens before the game even starts and it's a part of the singletone
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            //DontDestroyOnLoad(this);
            instance = this;
        }
    }
    #endregion
    
   #region CoRoutines
    /*//coroutine that returns if the
    IEnumerator AnimationDone()
    {
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0).Length); //waits for the length of the current animation before declaring animation bool to ture.
        animationDone = true;
        changeSprite = true;
        Debug.Log("changeSprite");
    }
    
   //coroutine that gives a buffer between each state switch.
    IEnumerator StateBuffer()
    {
        canSwitch = false;
        yield return new WaitForSeconds(0.1f);
        canSwitch = true;
    }*/
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myManager = PouringManager.FindInstance();
        //TransitionState(State.Default); //sets state to default.
    }

    #region StateMachine
    /*private void TransitionState(State newstate)
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
                    animator.Play("TeaPot Body", 0, 0f); //plays pouring animation.
                    pourAnimator.Play("pour_animation", 0, 0f); //plays the liquid pouring.
                    StartCoroutine(AnimationDone()); //checks if animation is done
                    if (animationDone) //if so
                    {
                        StartCoroutine(StateBuffer()); //implement the state buffer.
                        animationDone = false; //sets animationdone back to false. 
                    }
                    break;
                case State.Reset:
                    animator.Play("ResetPot", 0, 0f);   //plays reset animation
                    TransitionState(State.Default); //sets state back to default
                    break;
            }
        }
    }*/
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
        if (col.gameObject.CompareTag("cup")) //if the object is a cup 
        {
            //TransitionState(State.Pouring);
            _myManager.TransitionState(PouringManager.State.Pouring);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cup"))
            {
                //TransitionState(State.Reset);
                _myManager.TransitionState(PouringManager.State.Reset);
            }
    }
    #endregion
}

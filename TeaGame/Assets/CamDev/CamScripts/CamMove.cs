using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{


    //do tween
    public Animator animator;
    
    Vector3 mousePosOffset;
    private float mouseZ;

    public enum State
    {
        Default,
        Pouring,
        Reset
    }
    // Start is called before the first frame update
    void Start()
    {
        TransitionState(State.Default);
    }

    public void TransitionState(State newstate)
    {
        //currentState = newstate
    }

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


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cup"))
        {
            animator.SetBool("pouring", true);
            //animator.SetTrigger("pouring");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cup"))
        {
            animator.SetTrigger("reset");
            animator.SetBool("pouring", false);
            //animator.SetTrigger("reset");
        }
    }
}

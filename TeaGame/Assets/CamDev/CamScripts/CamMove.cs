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

    // Start is called before the first frame update
    void Start()
    {

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
        if (col.gameObject.CompareTag("Cup"))
        {
            animator.SetTrigger("pouring");
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Poop");
        /*if (col.gameObject.CompareTag("Cup"))
        {
            animator.SetTrigger("pouring");
        }*/
    }
}

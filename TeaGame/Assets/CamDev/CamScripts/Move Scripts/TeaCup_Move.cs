using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TeaCup_Move : CamMove
{
    [SerializeField] 
    private GameManager manager;
    [SerializeField]
    private SpriteController cupSpr;

    public Vector3 sipPos;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        /*
        //if the tea is not finished steeping, on mouse click the tea will stop steeping.
        if (steepManager._finishedSteep == false) //if it's false
        {
            steepManager._finishedSteep = true; //stops the steeping.
        }*/
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            Debug.Log("cheese;");
        }
        
        /*if (col.gameObject.CompareTag("Character"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.transform.position = sipPos;
            
            if (manager.currentState == GameManager.State.Resting)
            {
                manager.TransitionState(GameManager.State.Drinking);
            }
            Debug.Log("You gave tea");
        }*/
    }
}

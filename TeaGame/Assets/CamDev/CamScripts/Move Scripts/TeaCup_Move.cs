using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCup_Move : CamMove
{
    [SerializeField] 
    private Cam_Steep_Manager steepManager;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        //if the tea is not finished steeping, on mouse click the tea will stop steeping.
        if (steepManager._finishedSteep == false) //if it's false
        {
            steepManager._finishedSteep = true; //stops the steeping.
        }
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}

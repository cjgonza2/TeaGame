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

    [SerializeField] private GameObject pourCollider;

    public Vector3 sipPos;
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        pourCollider.SetActive(false);

        /*
        //if the tea is not finished steeping, on mouse click the tea will stop steeping.
        if (steepManager._finishedSteep == false) //if it's false
        {
            steepManager._finishedSteep = true; //stops the steeping.
        }*/
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        pourCollider.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            Debug.Log("cheese;");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot_Move : CamMove
{
    [SerializeField] 
    private Rigidbody2D teaPot;

    [SerializeField] 
    private Pot_SpriteChanger sprChanger;

    public override void Start()
    {
        _myManager = PouringManager.FindInstance();
        teaPot = gameObject.GetComponent<Rigidbody2D>();
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cup"))
        {
            _myManager.TransitionState(PouringManager.State.Pouring);
            teaPot.constraints = RigidbodyConstraints2D.FreezePosition;
            Debug.Log(teaPot.constraints);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cup"))
        {
            _myManager.TransitionState(PouringManager.State.Reset);
        }
    }
}

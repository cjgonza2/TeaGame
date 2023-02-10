using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot_Move : CamMove
{
    [SerializeField]
    public PouringManager _myManager;
    [SerializeField] 
    private Cam_Steep_Manager _steepManager;
    [SerializeField] 
    private Pot_SpriteChanger sprTrack;
    
    [SerializeField] 
    private Rigidbody2D teaPot;

    [SerializeField]
    private CircleCollider2D pourCollider; //reference to the collider the kettle will interact with.

    [SerializeField] 
    private Lid_Move lid;

    public override void Start()
    {
        _myManager = PouringManager.FindInstance();
        teaPot = gameObject.GetComponent<Rigidbody2D>();
        //pourCollider.enabled = true;
    }

    public override void Update()
    {
        base.Update(); //Does everything parent script does.

        /*if (_myManager.CurrentState == PouringManager.State.Resting && sprTrack._filled || lid._colliding)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (sprTrack._filled == false && lid._colliding == false)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }*/
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown(); //Does everything parent function does. 
        //pourCollider.enabled = false; //disables pouring collider. 
        
        if (sprTrack._steeping && _steepManager._finishedSteep == false)
        {
            //Debug.Log("you've clicekd me");
            //_steepManager._finishedSteep = true;
        }
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp(); //Does everything parent function does.
        //pourCollider.enabled = true; //enables pouring collider. 
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("cup")) //if colliding with a cup;
        {
            if (sprTrack._steeping)
            {
                _myManager.TransitionState(PouringManager.State.Pouring);
                _steepManager._finishedSteep = true;
            }
            //teaPot.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cup"))
        {
            if (sprTrack._steeping)
            {
                _myManager.TransitionState(PouringManager.State.Reset);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TeaPot_Move : CamMove
{
    [SerializeField]
    private PouringManager pourManager;
    [SerializeField] 
    private Cam_Steep_Manager _steepManager;
    [SerializeField] 
    private Pot_SpriteChanger sprTrack;

    [SerializeField] private GameManager myManager;

    [SerializeField]private GameObject teaPotLid;
    private bool _lidMoved = false;


    // pot/lid position coordinates.
    #region Lid Transform Position
    private float _teaLidXPos()
    {
        return teaPotLid.transform.position.x;
    }

    private float _teaLidYPos()
    {
        return teaPotLid.transform.position.y;
    }

    private float _teaLidZPos()
    {
        return teaPotLid.transform.position.z;
    }
    #endregion

    #region Pot Transform Position
    private float _PotX()
    {
        return transform.position.x;
    }

    private float _PotY()
    {
        return transform.position.y;
    }

    private float _PotZ()
    {
        return transform.position.z;
    }
    #endregion


    public override void Start()
    {
        base.Start(); //does everything parent function does. 
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //assigns game manager reference.
        teaPotLid = GameObject.Find("teapot_lid"); //assigns teapot lid reference;
        //_myManager = PouringManager.FindInstance();
    }

    public override void Update()
    {
        base.Update(); //Does everything parent script does.

        
        /*if (pourManager.CurrentState == PouringManager.State.KettlePour && _lidMoved == false)
        {
            _lidMoved = true;
            StartCoroutine(MoveLid());
        }

        if (pourManager.CurrentState == PouringManager.State.KettleReset) ;
        {
            StartCoroutine(ResetLidTest());
        }*/

        /*if (myManager.pouring == false && _lidMoved == true)
        {
            if (teaPotLid.transform.position != transform.position)
            {
                /*StartCoroutine(ResetLidTest());#1#
            }
        }*/
        
        //TestLidInput();  //this is to test the lid tweening. Phasing out the animation system.


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
                //_myManager.TransitionState(PouringManager.State.Pouring);
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
                //_myManager.TransitionState(PouringManager.State.Reset);
            }
        }
    }

    #region TweenTesting

    //private bool _spaceinput;
    IEnumerator MoveLid()
    {
        Debug.Log(_lidMoved);
        teaPotLid.transform.DOMove(new Vector3(
            _teaLidXPos() + 0.75f,
            _teaLidYPos() + 0.75f,
            _teaLidZPos()), 1f).SetEase(Ease.InOutCubic);
        teaPotLid.transform.DORotate(new Vector3(0, 0, -25), 1f);
        yield return new WaitForSeconds(0.1f);
        //myManager.lidMoved = true;
        
        //_spaceinput = true;
    }
    IEnumerator ResetLidTest()
    {
        yield return new WaitForSeconds(0.1f);
        teaPotLid.transform.DOMove(new Vector3(
            _PotX(),
            _PotY(),
            _PotZ()), 1f).SetEase(Ease.InOutCubic);
        teaPotLid.transform.DORotate(new Vector3(0, 0, 0), 1f);

    }


    #endregion
}

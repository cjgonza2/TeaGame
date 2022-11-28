using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle_Move : CamMove
{
    [SerializeField]
    private Vector3 startpos;
    [SerializeField]
    private bool _dragging = false;

    public bool _pouring = false;

    public override void Start()
    {
        base.Start();
        startpos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
            gameObject.transform.position.z);
    }

    private void Update()
    {
        if (_dragging == false)
        {
            gameObject.transform.position = startpos;
        }
    }

   public override void OnMouseDown()
   {
       _dragging = true;
       mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
       mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();
   }

   private void OnMouseUp()
   {
       _dragging = false;
   }

   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TeaPot"))
        {
            _myManager.TransitionState(PouringManager.State.KettlePour);
            _pouring = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TeaPot"))
        {
            _myManager.TransitionState((PouringManager.State.KettleReset));
            _pouring = false;
        }
    }
}

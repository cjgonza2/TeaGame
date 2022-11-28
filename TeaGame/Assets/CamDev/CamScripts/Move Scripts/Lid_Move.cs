using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid_Move : CamMove
{
    [SerializeField] 
    private GameObject _teaParent;

    private bool _dragging = false;

    public bool _colliding;
    // Start is called before the first frame update
    public override void Start()
    {
        _teaParent = GameObject.Find("teapot"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging == false)
        {
            gameObject.transform.position = _teaParent.transform.position;
        }
        Debug.Log("Dragging" + _dragging);
        Debug.Log("Colliding" + _colliding);
    }

    public override void OnMouseDown()
    {
        _dragging = true;
        _colliding = false;
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();
        
    }

    private void OnMouseUp()
    {
        if (_colliding)
        {
            _dragging = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == ("TeaPot"))
        {
            _colliding = true;
        }
    }
}

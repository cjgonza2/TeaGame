using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class CamMove : MonoBehaviour
{
    public GameManager myManager;

    private Vector3 _mousePos; //vector 3 of the mouse. 
    [HideInInspector]
    public bool _selected = false; //bool that determines if an object has been selected.

    private Camera _mainCam; //reference to main camera;
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public virtual void Update()
    {
        if (myManager.gamePaused)
        {
            _selected = false;
        }
        
        if (_selected) //if the an object is selected. 
        {
            CalculateMousePos();
        }
    }

    public virtual void CalculateMousePos() //calculates mouse position.
    {

        transform.position = _mainCam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            10));
        /*_mousePos = Input.mousePosition; //sets the mouse input position (mouse.x, mouse.y, 0)
        _mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; //sets the mouse z pos to the screen point of the selected gameobject's z position.
        transform.position = Camera.main.ScreenToWorldPoint(_mousePos)
                             - new Vector3(0, 0,
                                 transform.position.z);*/
        //sets the selected game objects' position to the converted world coordinates of the mouse. and ballances it with a z offset.
    }

    public virtual void OnMouseDown()
    {
        if (myManager.gamePaused) return;
        _selected = true;
    }

    public virtual void OnMouseUp()
    {
        _selected = false;
    }
}

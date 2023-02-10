using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    //This script is complete. 
    private Vector3 _mousePos; //vector 3 of the mouse. 
    [HideInInspector]
    public bool _selected = false; //bool that determines if an object has been selected.


    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (_selected) //if the an object is selected. 
        {
            CalculateMousePos();
        }
    }

    public virtual void CalculateMousePos() //calculates mouse position.
    {

        _mousePos = Input.mousePosition; //sets the mouse input position (mouse.x, mouse.y, 0)
        _mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; //sets the mouse z pos to the screen point of the selected gameobject's z position.
        transform.position = Camera.main.ScreenToWorldPoint(_mousePos)
                             - new Vector3(0, 0,
                                 transform.position.z); //sets the selected game objects' position to the converted world coordinates of the mouse. and ballances it with a z offset.
    }

    public virtual void OnMouseDown()
    {
        _selected = true;
    }

    public virtual void OnMouseUp()
    {
        _selected = false;
    }
}

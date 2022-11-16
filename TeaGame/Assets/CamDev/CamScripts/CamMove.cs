using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    //do tween - an extension for tweening that might be worth looking into.

    [SerializeField]
    public PouringManager _myManager;

    Vector3 mousePosOffset;
    private float mouseZ;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _myManager = PouringManager.FindInstance();
    }

    #region Obj Movement
    private void OnMouseDown()
    {
        Debug.Log("click");
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();
    }
    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return world point
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        Debug.Log("drag");
        transform.position = GetMouseWorldPosition() + mousePosOffset;
    }
    #endregion
}

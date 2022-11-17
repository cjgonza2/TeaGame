using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaLeavesController : MonoBehaviour
{
    public GameObject tea;
    GameObject teaClump;
    public bool clumpExists = false;

    Vector3 mousePosOffset;
    private float mouseZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return world point
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZ;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDown()
    {
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();

        if (clumpExists == false)
        {
            teaClump = Instantiate(tea, GetMouseWorldPosition(), Quaternion.identity);
            clumpExists = true;
        }
    
    }

    private void OnMouseDrag()
    {
        teaClump.transform.position = GetMouseWorldPosition() + mousePosOffset;
    }



}

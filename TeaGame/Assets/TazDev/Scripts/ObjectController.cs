using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    Vector3 mousePosOffset;
    private float mouseZ;
    public bool isCollided = false;

    #region SingletonDeclaration 
    private static ObjectController instance;
    public static ObjectController FindInstance()
    {
        return instance; //that's just a singletone as the region says
    }

    void Awake() //this happens before the game even starts and it's a part of the singletone
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            //DontDestroyOnLoad(this);
            instance = this;
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("cup") == true)
        {
            //Debug.Log("hit");
            isCollided = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
        Debug.Log("not collided");
    }*/
}

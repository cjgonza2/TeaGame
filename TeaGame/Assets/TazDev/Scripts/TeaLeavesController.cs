using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaLeavesController : MonoBehaviour
{
    [SerializeField]private GameManager inventoryManager;
    
    public GameObject tea;
    GameObject teaClump;
    private SpriteRenderer teaLayer;
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
        //Debug.Log("What the fuck");
        mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();
        //HakaCountCheck();
        if (clumpExists)
        {
            return;
        }
        
        teaClump = Instantiate(tea, GetMouseWorldPosition(), Quaternion.identity); //instantiates clone of clump prefab.
        teaLayer = teaClump.GetComponent<SpriteRenderer>(); //grabs the sprite renderer of cloned prefab.
        teaLayer.sortingLayerName = "Clumps"; //sets the sorting layer of prefab clone.
        clumpExists = true;
    
    }

    private void InventoryCheck()
    {
        
    }

    /*private void HakaCountCheck()
    {
        if (tea.name != "Haka")
        {
            return;
        }
        
        //Debug.Log("You just picked up a haka leaf.");
    }*/

    private void OnMouseDrag()
    {
        teaClump.transform.position = GetMouseWorldPosition() + mousePosOffset;
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaLeavesController : MonoBehaviour
{
    [SerializeField] private GameManager inventoryManager;

    [SerializeField] private GameObject highLight;
    [SerializeField] private GameObject tea;

    [SerializeField] private string leafName;

    [SerializeField] private bool clumpExists;

    [SerializeField] private bool enoughClumps;
    [SerializeField] private bool selected;

    private Camera _mainCam;
    
    private GameObject _teaClump;
    private SpriteRenderer _teaLayer;
    private Vector3 _mousePosOffset;
    private float _mouseZ;

    private void Awake()
    {
        highLight.SetActive(false);
    }


    //start/update in case we need it. 
    #region Start/Update
    public virtual void Start()
    {
        inventoryManager = GameObject.Find("GameManager").GetComponent<GameManager>();//finds the instance of the gameManager.
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public virtual void Update()
    {
        if (inventoryManager.gamePaused)
        {
            selected = false;
        }
        
        if (!selected)
        {
            return;
        }
        CalculateMousePos();
        //_teaClump.transform.position = GetMouseWorldPosition() + _mousePosOffset;

    }
    #endregion


    private void CalculateMousePos()
    {
        _teaClump.transform.position = _mainCam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            10));
    }

    #region Ingredient Checks
    /*private IEnumerator HakaCheck()
    {
        //Debug.Log("Haka Check");
        //switch expression.
        //Why a switch expression vs a switch statement?
        //Switch statements are good if for every case you need something to happen.
        //Switch expression are good if you just need to return a value.
        enoughClumps = inventoryManager.hakaCount switch
        {
            > 0 => true,
            <= 0 => false
        };
        yield break;
    }

    private IEnumerator TallowCheck()
    {
        Debug.Log("tallow Check");
        enoughClumps = inventoryManager.tallowCount switch
        {
            > 0 => true,
            <= 0 => false
        };
        yield break;
    }

    private IEnumerator BomBomCheck()
    {
        Debug.Log("bombom Check");
        enoughClumps = inventoryManager.bombomCount switch
        {
            > 0 => true,
            <= 0 => false
        };
        yield break;
    }

    private IEnumerator AileCheck()
    {
        enoughClumps = inventoryManager.aileCount switch
        {
            > 0 => true,
            <= 0 => false
        };
        yield break;
    }

    private IEnumerator ShnootCheck()
    {
        enoughClumps = inventoryManager.shnootCount switch
        {
            > 0 => true,
            <= 0 => false
        };
        yield break;
    }

    private IEnumerator PoffCheck()
    {
        enoughClumps = inventoryManager.poffCount switch
        {
            > 0 => true,
            <= 0 => false
        };
        yield break;
    }*/

    

    #endregion

    private void OnMouseOver() //while the mouse is over;
    {
        //sets the highlight to active.
        highLight.SetActive(true);
    }

    private void OnMouseExit() //when the mouse moves away from the object;
    {
        highLight.SetActive(false); //deactivates highlight objects.
    }

    public virtual void OnMouseDown()
    {
        if (inventoryManager.gamePaused) return;
        if (inventoryManager.compendiumOpen) return;
        
        //based on whatever leaf name there is, starts the appropriate check.
        //StartCoroutine($"{leafName}Check");

        /*if (!enoughClumps) //if it sees there aren't enough clumps.
        {
            return; //simply returns and doesn't do the rest.
        }*/

        //Debug.Log("YOu have made it bast the clump check.");

        selected = true; 
        
        if (clumpExists) //if the tea clump already exists;
        {
            return; //returns the function here.
        }
        
        CreateClump(); //creates the tea clump.
    
    }

    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return world point
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZ;
        return _mainCam.ScreenToWorldPoint(mousePoint);
    }
    
    private void CreateClump()
    {
        _teaClump = Instantiate(tea, GetMouseWorldPosition(), Quaternion.identity); //instantiates clone of clump prefab.
        _teaLayer = _teaClump.GetComponent<SpriteRenderer>(); //grabs the sprite renderer of cloned prefab.
        _teaLayer.sortingLayerName = "Clumps"; //sets the sorting layer of prefab clone.
        clumpExists = true;
    }

    private void OnMouseUp()
    {
        selected = false;
    }




}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class TeaClump : MonoBehaviour
{
    [SerializeField] 
    private Pot_SpriteChanger potSpr; //this is so we can access the 

    [SerializeField] 
    private Cam_Steep_Manager potSteep; //lets us pass the flavor to steep manager.

    [SerializeField] private GameManager inventoryManager;

    private bool _holdingClump = false;
    
    public GameObject lid;
    private float _lidX()
    {
        return lid.transform.position.x;
    }
    private float _lidY()
    {
        return lid.transform.position.y;
    }
    private float _lidZ()
    {
        return lid.transform.position.z;
    }

    private bool _holdLid = false;

    #region TeaPot GameObject/Coordinates
    public GameObject teaPot;
    private float _potX()
    {
        return teaPot.transform.position.x;
    }
    private float _potY()
    {
        return teaPot.transform.position.y;
    }
    private float _potZ()
    {
        return teaPot.transform.position.z;
    }
    #endregion

    
    public Vector2 restPos;
    public Vector2 inPotPos;

    private void Awake()
    {
        //Debug.Log(gameObject.tag);
        _holdingClump = true;
        restPos = new Vector2(0, -7.5f);
        teaPot = GameObject.Find("TeaPot");
        lid = GameObject.Find("TeaPotLid");
        potSpr = teaPot.GetComponent<Pot_SpriteChanger>();
        potSteep = teaPot.GetComponent<Cam_Steep_Manager>();
        inventoryManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y < restPos.y)
        {
            gameObject.transform.position = restPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        TeaPotCollision(col.transform.tag);
        
        if (potSteep.teaBase && potSteep.teaIng)
        {
            potSpr._steeping = true; //sets the teapot to steeping. Steeping counter starts.
        }

    }

    private void TeaPotCollision(string tag)
    {
        if (tag != "TeaPot")
        {
            return;
        }
        //hakachekc
        //tallow
        //
        SetBase(transform.tag);
        SetIngredient(transform.tag);
        InventoryCheck(transform.tag);
        transform.position = restPos;
    }
    
    private void SetBase(string teaBase)
    {
        if (potSteep.teaBase)
        {
            return;
        }
        switch (teaBase)
        {
            case "bitter":
                potSteep.bitter = true;
                potSteep.teaBase = true;
                break;
            case "mild":
                potSteep.mild = true;
                potSteep.teaBase = true;
                break;
            case "sweet":
                potSteep.sweet = true;
                potSteep.teaBase = true;
                break;
            default:
                Debug.Log("No base Detected");
                break;
        }
    }

    private void SetIngredient(string teaIng)
    {
        if (potSteep.teaIng)
        {
            return;
        }

        switch (teaIng)
        {
            case "sleep":
                Debug.Log("sleep");
                potSteep.sleep = true;
                potSteep.teaIng = true;
                break;
            case "energy":
                Debug.Log("energy");
                potSteep.energy = true;
                potSteep.teaIng = true;
                break;
            case "health":
                Debug.Log("health");
                potSteep.health = true;
                potSteep.teaIng = true;
                break;
            default:
                Debug.Log("no ingredient detected.");
                break;
        }
    }

    private void InventoryCheck(string clumpName)
    {
        switch (clumpName)
        {
            case "bitter":
                inventoryManager.hakaCount--;
                break;
            case "mild":
                inventoryManager.tallowCount--;
                break;
            case "sweet":
                inventoryManager.bombomCount--;
                break;
            case "sleep":
                inventoryManager.shnootCount--;
                break;
            case "energy":
                inventoryManager.poffCount--;
                break;
            case "health":
                inventoryManager.poffCount--;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        MoveLid(col.transform.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (!_holdingClump)
        {
            return;
        }*/
        
        ResetLid();
    }

    private void MoveLid(string tag)
    {
        if (tag != "TeaPotLid")
        {
            return;
        }
        lid.transform.DOMove(new Vector3( //tweens lid up to the right. 
            _lidX() + 1f,
            _lidY() + 1f,
            _lidZ()), 0.5f).SetEase(Ease.InOutCubic);
        lid.transform.DORotate(new Vector3(0, 0, -25), .5f); //rotates lid. 
    }

    private void ResetLid()
    {
        /*Debug.Log(_potX());
        Debug.Log(_potY());
        Debug.Log(_potZ());*/
        lid.transform.DOMove(new Vector3(
            _potX(),
            _potY(),
            _potZ()), 0.5f).SetEase(Ease.InOutCubic);
        lid.transform.DORotate(Vector3.zero, .5f);
    }
}

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
        return teaPot.transform.position.y;
    }
    #endregion

    
    public Vector2 restPos;
    public Vector2 inPotPos;

    private void Awake()
    {
        //Debug.Log(gameObject.tag);
        restPos = new Vector2(0, -7.5f);
        teaPot = GameObject.Find("teapot");
        lid = GameObject.Find("teapot_lid");
        potSpr = teaPot.GetComponent<Pot_SpriteChanger>();
        potSteep = teaPot.GetComponent<Cam_Steep_Manager>();
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
        
        SetBase(transform.tag);
        SetIngredient(transform.tag);
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
        
        /*//Debug.Log("Function called");
        //Debug.Log(teaBase);
        //this checks the tag of the given leaf put in the pot.
        if (teaBase == "bitter")
        {
            potSteep.bitter = true;
        }
        else if (teaBase == "mild")
        {
            potSteep.mild = true;
        }
        else if (teaBase == "sweet")
        {
            potSteep.sweet = true;
        }
        //tells the steep manager that there is a tea base in the pot.
        potSteep.teaBase = true;*/
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
                break;
            case "energy":
                Debug.Log("energy");
                break;
            case "health":
                Debug.Log("health");
                break;
            default:
                Debug.Log("no ingredient detected.");
                break;
        }
        potSteep.teaIng = true;
        /*//checks the tag of the given leaf put in the pot. 
        if (teaIng == "sleep")
        {
            potSteep.sleep = true;
        }
        else if (teaIng == "energy")
        {
            potSteep.energy = true;
        }
        else if (teaIng == "health")
        {
            potSteep.health = true;
        }
        //tells the steep manager that there is a tea ingredient in the pto. 
        potSteep.teaIng = true;*/
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        MoveLid(col.transform.tag);
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
}

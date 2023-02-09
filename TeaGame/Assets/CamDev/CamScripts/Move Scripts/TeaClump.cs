using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeaClump : MonoBehaviour
{
    [SerializeField] 
    private Pot_SpriteChanger potSpr; //this is so we can access the 

    [SerializeField] 
    private Cam_Steep_Manager potSteep; //lets us pass the flavor to steep manager.
    
    private Lid_Move _lid;

    public GameObject teaPot;
    public GameObject lid;

    public Vector2 restPos;

    private void Awake()
    {
        //Debug.Log(gameObject.tag);
        restPos = new Vector2(0, -7.5f);
        teaPot = GameObject.Find("teapot");
        lid = GameObject.Find("teapot_lid");
        potSpr = teaPot.GetComponent<Pot_SpriteChanger>();
        potSteep = teaPot.GetComponent<Cam_Steep_Manager>();
        _lid = lid.GetComponent<Lid_Move>();
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
        if (col.transform.tag == ("TeaPot"))
        {
           //if the pot has water, and the lid is not on top:
            if (potSpr._filled && _lid._colliding == false)
            {
                #region Base & Ingredient Set
                //checks the clump's tag is equal to any of the tea bases.
                if (transform.tag.Equals("bitter") || 
                    transform.tag.Equals("mild") || 
                    transform.tag.Equals("sweet"))
                {
                    //Debug.Log(gameObject.tag);
                    //If there is no base in the pot yet:
                    if (potSteep.teaBase != true)
                    {
                        //sets the base based on the tag of the clump. 
                        SetBase(gameObject.tag);
                    }
                } 
                else if (transform.tag.Equals("sleep") || 
                         transform.tag.Equals("health") ||
                         transform.tag.Equals("energy"))
                {
                    SetIngredient(gameObject.tag);
                }
                #endregion

                //Debug.Log("Time to steep");
                if (potSteep.teaBase && potSteep.teaIng)
                {
                    potSpr._steeping = true; //sets the teapot to steeping. Steeping counter starts.
                }
                Destroy(this);
                gameObject.transform.position = restPos;
            }
            //potSpr._steeping = true;
        }
    }

    private void SetBase(string teaBase)
    {
        //Debug.Log("Function called");
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
        potSteep.teaBase = true;
    }

    private void SetIngredient(string teaIng)
    {
        //checks the tag of the given leaf put in the pot. 
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
        potSteep.teaIng = true;
    }
}

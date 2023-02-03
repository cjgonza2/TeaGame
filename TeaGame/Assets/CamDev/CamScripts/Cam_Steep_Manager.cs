using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cam_Steep_Manager : MonoBehaviour
{
    #region Script References
    [SerializeField] 
    //reference to pot sprite changer.
    private Pot_SpriteChanger mySprite;
    #endregion

    #region Bases and Ingredients
    [Header("Tea Bases")]
    public bool mild = false;
    public bool sweet = false;
    public bool bitter = false;

    [Header("Tea Ingredients")] 
    public bool sleep = false;
    public bool health = false;
    public bool energy = false;
    #endregion

    #region Tea Blends

    [Header("Bitter Blends")]
    public bool bitterSleep = false;
    public bool bitterHealth = false;
    public bool bitterEnergy = false;

    [Header("Mild Blends")]
    public bool mildSleep = false;
    public bool mildHealth = false;
    public bool mildEnergy = false;

    [Header("Sweet Blends")]
    public bool sweetSleep = false;
    public bool sweetHealth = false;
    public bool sweetEnergy = false;

    #endregion

    [Header("Component Checks")]
    #region Ingredient Checks
    //Checks if there is a tea base in the mix.
    public bool teaBase = false;
    //Checks if there is a tea ingredient in mix.
    public bool teaIng = false;
    #endregion
    
    #region Objects

    //reference to teacup.
    private GameObject teaCup;

    //reference to tea sprite renderer. 
    [SerializeField]
    private SpriteRenderer teaSprite;

    #endregion
    
    #region Bools
    [Header("Bool")]
    public bool _finishedSteep = false;
    
    [Header("Tea Flavors")]
    public bool lowFlavor = false;
    public bool highFlavor = false;
    #endregion

    #region Values
    [Header("Time Ints")]
    public int maxTime;
    public int roundedTime;
    public float steepTime;
    
    #endregion

    private IEnumerator FlavorCheck()
    {
        if (bitter) //if the tea base is bitter:
        {
            if (sleep) //and if the tea ingredient is sleep:
            {
                bitterSleep = true; //sets blend to bitterSleep.
            }  else if (health) //and if the tea ingredient is health:
            {
                bitterHealth = true; //sets blend to bitterHealth.
            }   else if (energy) //and if the tea ingredient is energy:
            {
                bitterEnergy = true; //sets blend to bitterEnergy.
            }
        }
        else if(mild)
        {
            if (sleep)
            {
                mildSleep = true;
            }  else if (health)
            {
                mildHealth = true;
            }   else if (energy)
            {
                mildEnergy = true;
            }
        }
        else if (sweet)
        {
            if (sleep)
            {
                sweetSleep = true;
            }  else if (health)
            {
                sweetHealth = true;
            }   else if (energy)
            {
                sweetEnergy = true;
            }
        }
        yield return null;
    }
    
    // Update is called once per frame
    void Update()
    {
        //if the tea has a base and an ingredient:
        if (teaBase && teaIng)
        {
            //begins flavor check.
            StartCoroutine(FlavorCheck());
        }
        
        //if the teapot has water and tea, and it is not steeped yet;
        if (mySprite._steeping && _finishedSteep == false)
        {
            //begins steeping. 
            Steeping();
        }

        //if the rounded steep time is less than 15;
        if (roundedTime < 15)
        {
            //sets the pot sprite to default filled sprite.
            mySprite.currentSprite = mySprite.potTea;
        }else if (roundedTime >= 15) //otherwise if it's greater than 15;
        {
            //sets sprite to darker fill. 
            mySprite.currentSprite = mySprite.teaHigh;
        }


        //if the pot has finished steeping:
        if (_finishedSteep)
        {
            //if the rounded time is less than 15;
            if (roundedTime < 15)
            {
                //sets the flavor to low. 
                lowFlavor = true;
            }
            else if (roundedTime >= 15) //otherwise:
            {
                //sets flavor to high. 
                highFlavor = true;
            }
        } ;
    }

    void Steeping()
    {
        //if pot has not finished steeping;
        if (_finishedSteep == false)
        {
            //adds one to the steep time based on delta time. . 
            steepTime += Time.deltaTime;
            //rounds the steep time to a whole number.
            roundedTime = (int)(steepTime % 60);
        }
        //Debug.Log(roundedTime);

        //if roundedtime is greater than or equal to the max time;
        if (roundedTime >= maxTime)
        {
            //finishes the steeping. 
            _finishedSteep = true;
        }
        
    }
}

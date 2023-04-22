using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cam_Steep_Manager : MonoBehaviour
{
    #region Script References
    [SerializeField]private Pot_SpriteChanger mySprite;//reference to pot sprite changer.
    #endregion

    [SerializeField] private GameObject lidCollider; //collider for the lid. 
    
    #region Bases, Ingredients, and Blends
    [Header("Component Checks")]
    public bool teaBase = false; //Checks if there is a tea base in the pot.
    public bool teaIng = false; //Checks if there is a tea ingredient in pot.
    
    [Header("Tea Bases")]
    public bool mild = false;
    public bool sweet = false;
    public bool bitter = false;

    [Header("Tea Ingredients")] 
    public bool sleep = false;
    public bool health = false;
    public bool energy = false;
    
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

    #region Objects

    
    private GameObject teaCup; //reference to teacup.

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


    public SpriteRenderer mainPotSpr;

    [Header("Dry Sprites")]
    [SerializeField] private List<Sprite> dryBaseSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> dryIngSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> dryTisaneSprites = new List<Sprite>();


    private void Update()
    {
        MixCheck();

        //if the teapot has water and tea, and it is not steeped yet;
        if (mySprite._steeping && !_finishedSteep)
        {
            Steeping(); //begins steeping. 
        }

        if (teaBase && teaIng && mySprite._filled)
        {
            mySprite._steeping = true;//sets the teapot to steeping. Steeping counter starts.
        }

        //if the pot has finished steeping:
        if (_finishedSteep)
        {
            switch (roundedTime)
            {
                //if the rounded time is less than 15;
                case < 16:
                    //sets the flavor to low. 
                    lowFlavor = true;
                    break;
                //otherwise:
                case >= 16:
                    //sets flavor to high. 
                    highFlavor = true;
                    break;
            }
        } ;
    }

    private void MixCheck()
    {
        //if the tea has a base and an ingredient:
        if (!teaBase) return;
        if (!teaIng) return;
        StartCoroutine(FlavorCheck()); //begins flavor check.
    }
    
    private IEnumerator FlavorCheck()
    {
        BitterCheck(); //calls bitter check.
        MildCheck(); //calls mild check.
        SweetCheck(); //calls sweet check.
        yield break; //breaks. 
    }

    #region BitterChecks
    private void BitterCheck()
    {
        if (!bitter) return; //if bitter base isn't in the pot; breaks.

        BitterSleepCheck();
        BitterHealthCheck();
        BitterEnergyCheck();

    }
    private void BitterSleepCheck()
    {
        if (!sleep) return; //if sleep ing isn't in the pot; breaks. 

        bitterSleep = true;
    }
    private void BitterHealthCheck()
    {
        if (!health) return; //if health ing isn't in the pot; breaks.

        bitterHealth = true;
    }

    private void BitterEnergyCheck()
    {
        if (!energy) return; //if energy ing isn't in the pot; breaks.\

        bitterEnergy = true;
    }
    #endregion

    #region MildChecks
    private void MildCheck()
    {
        if (!mild) return; //if mild base isn't in the pot; breaks.
        
        MildSleepCheck();
        MildHealthCheck();
        MildEnergyCheck();
    }
    private void MildSleepCheck()
    {
        if (!sleep) return; //if sleep ing isn't in the pot; breaks. 

        mildSleep = true;
    }

    private void MildHealthCheck()
    {
        if (!health) return; //if health ing isn't in the pot; breaks. 

        mildHealth = true;
    }

    private void MildEnergyCheck()
    {
        if (!energy) return; //if energy ing isn't in the pot; breaks.

        mildEnergy = true;
    }
    #endregion

    #region SweetChecks
    private void SweetCheck()
    {
        if (!sweet) return; //if sweet base isn't in the pot; breaks. 
        
        SweetSleepCheck();
        SweetHealthCheck();
        SweetEnergyCheck();
    }

    private void SweetSleepCheck()
    {
        if (!sleep) return; //if sleep ing isn't in the pot; breaks.

        sweetSleep = true;
    }

    private void SweetHealthCheck()
    {
        if (!health) return; //if health ing isn't in the pot; breaks. 

        sweetHealth = true;
    }

    private void SweetEnergyCheck()
    {
        if (!energy) return; //if energy ing isn't in the pot; breaks. 

        sweetEnergy = true;
    }
    #endregion
    
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

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cam_Steep_Manager : MonoBehaviour
{
    #region Script References
    [SerializeField] 
    private Pot_SpriteChanger mySprite;
    #endregion
    

    #region Objects

    private GameObject teaCup;

    [SerializeField] 
    private SpriteRenderer teaSprite;

    #endregion
    
    #region Bools
    [Header("Bool")]
    public bool _finishedSteep = false;
    
    [Header("Tea Flavors")]
    public bool lowFlavor = false;
    public bool medFlavor = false;
    public bool highFlavor = false;
    #endregion

    #region Values
    [Header("Time Ints")]
    public int maxTime;
    public int roundedTime;
    public float steepTime;
    
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (mySprite._steeping && _finishedSteep == false)
        {
            Steeping();
        }

        if (roundedTime < 15)
        {
            mySprite.currentSprite = mySprite.potTea;
        }else if (roundedTime >= 15 && roundedTime < 30)
        {
            mySprite.currentSprite = mySprite.teaMed;
        }else if (roundedTime >= 30)
        {
            mySprite.currentSprite = mySprite.teaHigh;
        }


        if (_finishedSteep)
        {
            if (roundedTime < 15)
            {
                lowFlavor = true;
            }
            else if (roundedTime >= 15 && roundedTime < 30)
            {
                medFlavor = true;
            }
            else if (roundedTime >= 30)
            {
                highFlavor = true;
            }
        } ;
    }

    void Steeping()
    {
        if (_finishedSteep == false)
        {
            steepTime += Time.deltaTime;
            roundedTime = (int)(steepTime % 60);
        }
        //Debug.Log(roundedTime);

        if (roundedTime >= maxTime)
        {
            _finishedSteep = true;
        }
        
    }
}

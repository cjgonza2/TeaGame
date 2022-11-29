using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Steep_Manager : MonoBehaviour
{
    #region Script References
    [SerializeField] 
    private Pot_SpriteChanger mySprite;
    #endregion
    

    #region Objects
    GameObject teaCup

    [SerializeField] 
    private SpriteRenderer teaSprite;

    #endregion
    
    #region Bools
    [Header("Bool")]
    public bool stop = false;
    
    private bool _finishedSteep = false;
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
    }

    void Steeping()
    {
        steepTime += Time.deltaTime;
        roundedTime = (int)(steepTime % 60);
        Debug.Log(roundedTime);

        if (roundedTime >= maxTime)
        {
            _finishedSteep = true;
        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    void Update()
    {
        //input checks.
        UpInput();
        LeftInput();
        RightInput();
    }

    #region Up Input
    private void UpInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) //if w key is pressed down;
        { 
            UpLook(); //lerps the camera up to the Customer. 
        }
        if (Input.GetKeyUp(KeyCode.W)) //if w key is released;
        {
            UpReset(); //lerps the camera down to start pos. 
        }
    }

    private void UpLook()
    {
        //tweens the object up to the customer.
        gameObject.transform.DOMoveY(9.5f, 0.5f).SetEase(Ease.InOutCubic);
    }

    private void UpReset()
    {
        //tweens the camera to start position.
        gameObject.transform.DOMoveY(1, 0.5f).SetEase(Ease.InOutCubic);
    }
    #endregion

    #region Left Input
    private void LeftInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftLook();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            LeftReset();
        }
    }
    private void LeftLook()
    {
        
    }

    private void LeftReset()
    {
        
    }
    #endregion

    #region Right Input
    void RightInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightLook();
        }
        
        if (Input.GetKeyUp(KeyCode.D))
        {
            RightReset();
        }
    }
    private void RightLook()
    {
        
    }

    private void RightReset()
    {
        
    }
    #endregion
}

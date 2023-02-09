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

    private Vector3 _startPos;
    
    [SerializeField]
    private float lookUpDistance;
    [SerializeField]
    private float lookLeftDistance;
    [SerializeField] 
    private float lookRightDistance;

    private float _startPosX;
    private float _startPosY;

    [SerializeField]
    private bool _input = false;

    private void Start()
    {
        _startPos = gameObject.transform.position;
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
    }

    void Update()
    {
        //input checks.
        UpInput();
        LeftInput();
        RightInput();
        /*if (_input == false && gameObject.transform.position != _startPos)
        {
            
        }*/
    }

    #region Up Input
    private void UpInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _input == false) //if w key is pressed down and no other input is pressed.;
        { 
            UpLook(); //lerps the camera up to the Customer.
        }

        if (Input.GetKeyUp(KeyCode.W) && _input) //if w key is released;
        {
            UpReset(); //lerps the camera down to start pos.
        }
    }

    private void UpLook()
    {
        _input = true; //sets input to true;
        //tweens the object up to the customer.
        gameObject.transform.DOMoveY(lookUpDistance, 0.5f).SetEase(Ease.InOutCubic);
        
    }

    private void UpReset()
    {
        //tweens the camera to start position.
        gameObject.transform.DOMoveY(_startPosY, 0.5f).SetEase(Ease.InOutCubic);
        _input = false;
    }
    #endregion

    #region Left Input
    private void LeftInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && _input == false)
        {
            LeftLook();
        }

        if (Input.GetKeyUp(KeyCode.A) && _input)
        {
            LeftReset();
        }
    }
    private void LeftLook()
    {
        _input = true;
        gameObject.transform.DOMoveX(lookLeftDistance, 0.5f).SetEase(Ease.InOutCubic);
        
    }

    private void LeftReset()
    {
        gameObject.transform.DOMoveX(_startPosX, 0.5f).SetEase(Ease.InOutCubic);
        _input = false;
    }
    #endregion

    #region Right Input
    void RightInput()
    {
        if (Input.GetKeyDown(KeyCode.D) && _input == false)
        {
            RightLook();
        }
        
        if (Input.GetKeyUp(KeyCode.D) && _input)
        {
            RightReset();
        }
    }
    private void RightLook()
    {
        _input = true;
        gameObject.transform.DOMoveX(lookRightDistance, 0.5f).SetEase(Ease.InOutCubic);
        
    }

    private void RightReset()
    {
        gameObject.transform.DOMoveX(_startPosX, 0.5f).SetEase(Ease.InOutCubic);
        _input = false;
    }
    #endregion
}

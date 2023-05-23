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

    [SerializeField] private GameManager myManager;
    
    private Vector3 _startPos;
    
    [SerializeField]
    private float lookUpDistance;
    [SerializeField]
    private float lookLeftDistance;
    [SerializeField] 
    private float lookRightDistance;

    private float _startPosX;
    private float _startPosY;

    private float _cameraX()
    {
        return gameObject.transform.position.x;
    }

    private float _cameraY()
    {
        return gameObject.transform.position.y;
    }

    [SerializeField]
    private bool _input = false;

    private bool _sceneEnd;

    private bool _customerLook;

    private void Start()
    {
        _startPos = gameObject.transform.position;
        _startPosX = transform.position.x;
        _startPosY = transform.position.y;
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        //input checks.
        if (myManager.compendiumOpen) return;

        if (myManager.currentState == GameManager.State.Drinking)
        {
            LookAtCustomer();
            return;
        }

        if (_sceneEnd) return;
        DownInput();
        UpInput();
        LeftInput();
        RightInput();
    }

    private void LookAtCustomer()
    {
        if (_customerLook) return;
        _customerLook = true;
        _input = false;
        _sceneEnd = true;
        gameObject.transform.DOMoveY(lookUpDistance, 0.5f).SetEase(Ease.InOutCubic);
    }
    
    private void DownInput()
    {
        if (gameObject.transform.position.y != lookUpDistance) return;
        if (Input.GetKeyUp(KeyCode.S))
        {
            gameObject.transform.DOMoveY(_startPosY, 0.5f).SetEase(Ease.InOutCubic);
        }
        
    }
    
    
    #region Up Input
    private void UpInput()
    {
        if (gameObject.transform.position != _startPos) return; //if the camera's position is not the start position.
        if (Input.GetKeyDown(KeyCode.W)) //if w key is pressed down;
        {
            UpLook(); //lerps the camera up to the Customer.
        }
    }

    private void UpLook()
    {
        //tweens the object up to the customer.
        gameObject.transform.DOMoveY(lookUpDistance, 0.5f).SetEase(Ease.InOutCubic);
        
    }
    #endregion

    #region Left Input
    private void LeftInput()
    {
        if (gameObject.transform.position == _startPos)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LeftLook();
            }
        }else if (Math.Abs(_cameraX() - lookRightDistance) < .01)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                gameObject.transform.DOMoveX(_startPosX, 0.5f).SetEase(Ease.InOutCubic);
            }
        }

    }
    private void LeftLook()
    {
        gameObject.transform.DOMoveX(lookLeftDistance, 0.5f).SetEase(Ease.InOutCubic);
    }
    #endregion

    #region Right Input
    private void RightInput()
    {
        if (gameObject.transform.position == _startPos)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                RightLook();
            }
        }else if (Math.Abs(_cameraY() - lookRightDistance) > .01)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                gameObject.transform.DOMoveX(_startPosX, 0.5f).SetEase(Ease.InOutCubic);
            }
        }
    }
    private void RightLook()
    {
        gameObject.transform.DOMoveX(lookRightDistance, 0.5f).SetEase(Ease.InOutCubic);
        
    }
    #endregion
}

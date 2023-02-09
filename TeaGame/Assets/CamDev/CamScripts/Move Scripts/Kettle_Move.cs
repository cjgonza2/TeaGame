using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kettle_Move : CamMove
{
    
    [SerializeField]
    private ParticleSystem lowBoil;
    [SerializeField]
    private ParticleSystem medBoil;
    [SerializeField] 
    private ParticleSystem highBoil;
    [SerializeField]
    private Vector3 startpos;
    [SerializeField]
    private bool _dragging = false;

    public bool _pouring = false;
    [SerializeField]
    private bool boiling = false;
    
    
    #region Boil Values
    private float _boilCounter;
    
    public int boilTime;
    #endregion

    public override void Start()
    {
        base.Start();
        lowBoil.enableEmission = false;
        startpos = new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y,
            gameObject.transform.position.z);
    }

    private void Update()
    {
        if (_dragging == false)
        {
            gameObject.transform.position = startpos;
        }

        if (gameObject.transform.position == startpos)
        {
            _boilCounter += Time.deltaTime;
            boilTime = (int)(_boilCounter % 60);
        }

        if (gameObject.transform.position != startpos)
        {
            _boilCounter -= Time.deltaTime;
            boilTime = (int)(_boilCounter % 60);
        }

        if (_boilCounter < 0)
        {
            _boilCounter = 0;
        }
        
        

        if (boilTime > 5)
        {
            lowBoil.enableEmission = true;
            
        }

        if (boilTime > 15)
        {
            medBoil.enableEmission = true;
        }
        else
        {
            medBoil.enableEmission = false;
        }

        if (boilTime > 20)
        {
            highBoil.enableEmission = true;
            boiling = true;
        }
        else
        {
            highBoil.enableEmission = false;
            boiling = false;
        }
    }

   public override void OnMouseDown()
   {
       _dragging = true;
       mouseZ = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
       mousePosOffset = gameObject.transform.position - GetMouseWorldPosition();
   }

   private void OnMouseUp()
   {
       _dragging = false;
   }

   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TeaPot"))
        {
            if (boiling)
            {
                _myManager.TransitionState(PouringManager.State.KettlePour);
                _pouring = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TeaPot"))
        {
            _myManager.TransitionState((PouringManager.State.KettleReset));
            _pouring = false;
        }
    }
}

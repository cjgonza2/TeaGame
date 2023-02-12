using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Kettle_Move : CamMove
{

    //private Rigidbody2D myBody;
    private Vector3 _burnerPos;

    private Vector3 _startPos;
    
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

    private bool _onBurner = false;
    
    #region Boil Values
    private float _boilCounter; //raw number to count how long kettle has been boiling.
    [SerializeField]
    private int boilTime; //rounded number for counting how long kettle has been boiling.
    
    #endregion

    //this is only temporary until boiling sprite animation is done.
    IEnumerator BeginBoil() //enables initial particle system for boiling.
    {
        var emission = lowBoil.emission;
        emission.enabled = true;
        Debug.Log("boil has started.");
        yield break;
    }
    
    public override void Start()
    {
        base.Start(); //does everything parent function does.
        Debug.Log(_startPos);
        _startPos = gameObject.transform.position;
        StartCoroutine(BeginBoil());
        //transform.DORotate(new Vector3(0, 0, 120), 2f);

        
        /*
        startpos = new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y,
            gameObject.transform.position.z);
            */

    }

    public override void Update()
    {
        base.Update(); //does everything parent function does.

        Debug.Log("Boiling:" + boiling  );
        Debug.Log("Selected:" + _selected);
        if (_onBurner && _selected == false) //if kettle is on the burner, and it's not selected;
        {
            transform.DOMove(_startPos, 0.1f).SetEase(Ease.Linear); //moves the kettle to the burner.
            boiling = true; //kettle starts boiling.
        }
        else //otherwise;
        {
            boiling = false; //kettle does not boil.
        }

        Boiling();

        /*
        if (gameObject.transform.position == startpos)
        {
            _boilCounter += Time.deltaTime;
            boilTime = (int)(_boilCounter % 60);
        }

        if (gameObject.transform.position != startpos)
        {
            _boilCounter -= Time.deltaTime;
            boilTime = (int)(_boilCounter % 60);
        }*/

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

    private void Boiling()
    {
        if (boiling) //if the kettle is boiling;
        {
            _boilCounter += Time.deltaTime; //adds to the boil counter by rate of time between frames.
        }else if (boiling == false) //otherwise;
        {
            _boilCounter -= Time.deltaTime; //lowers the boil counter by rate of time between frames. 
        }

        if (_boilCounter < 0)
        {
            _boilCounter = 0;
        }

        boilTime = (int)(_boilCounter % 60); //converts boil counter into rounded whole number.
    }

    //Here in case we need to use it.
    public override void OnMouseDown()
   {
       base.OnMouseDown();
   }

    //here in case we need to use it.
    public override void OnMouseUp()
    {
        base.OnMouseUp();
    }

   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TeaPot"))
        {
            if (boiling)
            {
                Debug.Log("Fuckiuty fuck");
                
                //_myManager.TransitionState(PouringManager.State.KettlePour);
                _pouring = true;
            }
        }

        if (col.transform.tag.Equals("Burner")) //if kettle enters burner trigger;
        {
            _onBurner = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag.Equals("Burner")) //if Kettle exits the burner trigger;
        {
            _onBurner = false;
        }
        
        if (other.gameObject.CompareTag("TeaPot"))
        {
            //_myManager.TransitionState((PouringManager.State.KettleReset));
            _pouring = false;
        }
    }
}

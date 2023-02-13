using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Kettle_Move : CamMove
{
    
    private Vector3 _startPos; //default position of the Kettle.

    [SerializeField] private Animator kettleLiquid;
    
    //Thinking in the future instead of particle system we can just use different steam animations.
    /*[SerializeField] private Sprite lowSteam;
    [SerializeField] private Sprite medSteam;
    [SerializeField] private Sprite highSteam;*/
    
    //probably won't need these in the future. but for now they work to simulate boiling.
    [SerializeField]
    private ParticleSystem lowBoil;
    [SerializeField]
    private ParticleSystem medBoil;
    [SerializeField] 
    private ParticleSystem highBoil;
    
    public bool _pouring = false;
    
    [SerializeField] //tracks if the kettle is boiling.
    private bool boiling = false;
    [SerializeField] //tracks if the kettle is boiled.
    private bool boiled = false;

    private bool _onBurner = false; //tracks if the kettle is on the burner.
    
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

        Boiling(); //calculates boiling time.
        Steam(); //enables steam && determines if kettle is fully boiled. 
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

        if (_boilCounter < 0) //if boil counter is less than 0
        {
            _boilCounter = 0; //resets boil counter to 0.
        }

        boilTime = (int)(_boilCounter % 60); //converts boil counter into rounded whole number.
    }
    
    public void Steam()
    {
        if (boilTime > 5) //if kettle's been boiling for 5 seconds or more, 
        {
            lowBoil.enableEmission = true; //enables low steam.
        }
        else
        {
            lowBoil.enableEmission = false; //otherwise disables.
        }
        
        if (boilTime > 15) //if kettle's been boiling for 5 seconds
        {
            medBoil.enableEmission = true; //enables med steam.
        }
        else
        {
            medBoil.enableEmission = false; //otherwise disables.
        }

        if (boilTime > 20) //if kettle's been boiling for 5 seconds or more,
        {
            highBoil.enableEmission = true; //enables high steam.
            boiled = true; //kettle is now boiled. 
        }
        else
        {
            highBoil.enableEmission = false; //otherwise disables. 
            boiled = false; //kettle is no longer boiled.
        }
        
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

    IEnumerator KettlePour()
    {
        Debug.Log("kettle is pouring now.");
        transform.DORotate(new Vector3(0, 0, -25f), 0.5f).SetEase(Ease.InOutCubic);
        kettleLiquid.Play("water_pour", 0, 0f);
        yield return new WaitForSeconds(kettleLiquid.GetCurrentAnimatorClipInfo(0).Length);
    }

    IEnumerator KettleReset()
    {
        //gotta reset the kettle's posiiton. do tomorrow.
        yield break;
    }
   private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TeaPot"))
        {
            if (boiled)
            {
                Debug.Log("Fuckiuty fuck");
                StartCoroutine(KettlePour());
                
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

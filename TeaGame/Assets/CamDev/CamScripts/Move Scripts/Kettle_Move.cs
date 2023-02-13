using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Kettle_Move : CamMove
{
    [SerializeField] private PouringManager pourManager;
    
    private Vector3 _startPos; //default position of the Kettle.

    [SerializeField] private Animator kettleLiquid;
    [SerializeField] private GameManager myManager;

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

    public GameObject teaPot;

    public float potX()
    {
        return teaPot.transform.position.x;
    }

    public float potY()
    {
        return teaPot.transform.position.y;
    }

    public float potZ()
    {
        return teaPot.transform.position.z;
    }

    public GameObject teaLid;

    public float lidX()
    {
        return teaLid.transform.position.x;
    }

    public float lidY()
    {
        return teaLid.transform.position.y;
    }

    public float lidZ()
    {
        return teaLid.transform.position.z;
    }

    public override void Start()
    {
        base.Start(); //does everything parent function does.
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //assigns Gamemanager reference.
        //Debug.Log(_startPos);
        _startPos = gameObject.transform.position; //sets the startpos to the current kettle position.
        StartCoroutine(BeginBoil()); //starts the kettle boiling.
    }

    //this is only temporary until boiling sprite animation is done.
    IEnumerator BeginBoil() //enables initial particle system for boiling.
    {
        var emission = lowBoil.emission;
        emission.enabled = true;
        Debug.Log("boil has started.");
        yield break;
    }
    
    public override void Update()
    {
        base.Update(); //does everything parent function does.

        //Debug.Log("Boiling:" + boiling  );
        //Debug.Log("Selected:" + _selected);

        #region Boiiling on/off
        if (_onBurner && _selected == false) //if kettle is on the burner, and it's not selected;
        {
            transform.DOMove(_startPos, 0.1f).SetEase(Ease.Linear); //moves the kettle to the burner.
            boiling = true; //kettle starts boiling.
        }
        else //otherwise;
        {
            boiling = false; //kettle does not boil.
        }
        #endregion

        Boiling(); //calculates boiling time.
        Steam(); //enables steam && determines if kettle is fully boiled. 
    }

    private void Boiling() //tracks the kettle's boiling.
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
    
    //again this will change when we have boiling animations. 
    private void Steam()
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

    #region Kettle Tweening

    

        #endregion
    IEnumerator KettlePour()
    {
        Debug.Log("kettle is pouring now.");
        transform.DORotate(new Vector3(0, 0, -25f), 0.5f).SetEase(Ease.InOutCubic);
        kettleLiquid.Play("water_pour", 0, 0f);
        yield return new WaitForSeconds(kettleLiquid.GetCurrentAnimatorClipInfo(0).Length);
        //pourManager.TransitionState(PouringManager.State.KettleReset);
    }

    IEnumerator KettleReset()
    {
        //gotta reset the kettle's posiiton. do tomorrow.
        yield return new WaitForSeconds(0.1f);
        transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutCubic);
    }

    #region Lid Tweening
    private void MoveLid()
    {
        teaLid.transform.DOMove(new Vector3( //tweens lid up to the right. 
            lidX() + 1f,
            lidY() + 1f,
            lidZ()), 0.5f).SetEase(Ease.InOutCubic);
        teaLid.transform.DORotate(new Vector3(0, 0, -25), .5f); //rotates lid. 
    }
    private void ResetLid()
    {
        teaLid.transform.DOMove(new Vector3( //tweens lid back to pot position. 
            potX(),
            potY(),
            potZ()), 0.5f).SetEase(Ease.InOutCubic);
        teaLid.transform.DORotate(Vector3.zero, .5f); //rotates lid back to correct rotation.
    }
    #endregion

    #region On Trigger Enter Events
    private void OnTriggerEnter2D(Collider2D col)
    {
        StartPouring(col.transform.tag);
        KettleOnBurner(col.transform.tag);
    }

    private void StartPouring(string tag)
    {
        //Debug.Log("kettle is supposed to start pouring.");
        if (tag != "TeaPotLid") //if tag is not Tea Pot Lid;
        {
            return; //stops function.
        }
        if (!boiled) //if the tag is correct but the kettle is not boiled;
        {
            return; //stops the function.
        }
        MoveLid(); //moves the tea lid.
        StartCoroutine(KettlePour()); //pours the kettle. 
    }

    private void KettleOnBurner(string tag)
    {
        if (tag != "Burner")
        {
            return;
        }
        _onBurner = true; //Sets bool true.
    }
    #endregion

    #region On Trigger Exit Events
   private void OnTriggerExit2D(Collider2D other)
    {
        KettleOffBurner(other.transform.tag);

        FinishPouring(other.transform.tag);
    }

   private void KettleOffBurner(string tag)
   {
       if (tag != "Burner")//if trigger's tag is not burner;
       {
           return;//stops function. 
       }
       _onBurner = false; //otherwise, sets bool to false.
   }
   
   private void FinishPouring(string tag)
   {
       //Debug.Log("finish pouring function called.");
       
       //this is what's called a guard clause. 
       //by saying if the tag is not "x" it will just return, funciton won't do anything
       //But in the event that it is "x" it can do everything else.
       //this is basically a way to get rid of a bunch of nesting if statements and cleaner code
       //Further note: return will terminate a function wherever the return line is. 
       //so since unity and most others read code one line at a time top to bottom, stops anything
       //bellow return from doing it's thing. 
       
       if (tag != "TeaPotLid") //if trigger's tag is not TeaPotLid;
       {
           return; //stops function.
       }
       ResetLid(); //resets the lid,
       StartCoroutine(KettleReset()); //resets kettle.
   }
   #endregion

}

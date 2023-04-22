using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Kettle_Move : CamMove
{
    
    [SerializeField] private Animator kettleLiquid;
    [SerializeField] private GameObject boilingWater;


    [SerializeField] private Vector3 pouringPos;
    private Vector3 _startPos; //default position of the Kettle.

    #region Sprite Variables
    [SerializeField] private Pot_SpriteChanger potSpriteChanger; //reference to sprite changer script.
    [SerializeField] private SpriteRenderer kettleSprite; //reference to GameObject Sprite Renderer.
    [SerializeField] private Sprite emptyKettle; //Empty kettle Sprite.
    [SerializeField] private Sprite fullKettle; //Full Kettle Sprite.
    private Sprite _currentSprite; //Current Sprite of the Kettle
    #endregion


    [Header("Steam Animators")] 
    [SerializeField] private GameObject lowSteam;
    [SerializeField] private GameObject medSteam;
    [SerializeField] private GameObject highSteam;

    public bool _pouring = false;
    public bool fill; //tells the game whether to fill the kettle or not.
    [SerializeField] private bool _filled;
    [SerializeField] //tracks if the kettle is boiling.
    private bool boiling = false;
    [SerializeField] //tracks if the kettle is boiled.
    private bool boiled = false;

    [SerializeField] private bool _onBurner = false; //tracks if the kettle is on the burner.
    
    #region Boil Values
    [SerializeField] private int boilTime; //rounded number for counting how long kettle has been boiling.
    private float _boilCounter; //raw number to count how long kettle has been boiling.
    #endregion

    #region TeaPot Variables
    [Header("TeaPot")]
    public GameObject teaPot; //Tea Pot Game Object.
    private float _potX() //returns the tea pots x pos.
    {
        return teaPot.transform.position.x;
    }

    private float _potY() //returns the tea pots y pos.
    {
        return teaPot.transform.position.y;
    }

    private float _potZ() //returns the tea pots z pos.
    {
        return teaPot.transform.position.z;
    }
    #endregion

    #region TeaLid Variables
    [Header("TeaLid")]
    [SerializeField] private GameObject teaLid; //Tea Pot Lid Game Object.
    private float _lidX() //returns lid's x pos.
    {
        return teaLid.transform.position.x;
    }

    private float _lidY() //returns lid's y pos.
    {
        return teaLid.transform.position.y;
    }

    private float _lidZ() //returns lid's z pos.
    {
        return teaLid.transform.position.z;
    }
    #endregion
    
    public override void Start()
    {
        base.Start(); //does everything parent function does.
        
        _startPos = gameObject.transform.position; //sets the startpos to the current kettle position.
    }

    public override void Update()
    {
        base.Update(); //does everything parent function does.
        
        FillCheck(); //checks if the kettle is filled.
        
        MoveKettleToBurner(); //moves the kettle if it's on the burner.

        Boiling(); //calculates boiling time.
        
        Steam(); //enables steam && determines if kettle is fully boiled. 
        
    }

    private void FillCheck()
    {
        if (!fill) return; //if we don't have to fill the kettle; breaks
        
        _currentSprite = fullKettle; //sets the current sprite to the full kettle sprite.
        
        kettleSprite.sprite = _currentSprite; //Updates kettle's sprite to current sprite.
        
        fill = false; //tells the game it no longer has to fill the kettle.  
        
        _filled = true; //the kettle is now filled.
    }

    private void MoveKettleToBurner()
    {
        if (!_onBurner) //if the kettle is not the burner;
        {
            boiling = false; //boiling is set to false,
            return; //breaks.
        }

        if (_selected) return; //if the kettle is selected; breaks. 

        transform.DOMove(_startPos, 0.1f).SetEase(Ease.Linear); //tweens kettle to start pos. 

        boiling = _filled switch //boiled is set to true if Kettle is filled, false if kettle is not filled.
        {
            true => true,
            false => false
        };
    }
    
    private void Boiling() //tracks the kettle's boiling.
    {
        switch (boiling) 
        {
            //if the kettle is boiling;
            case true:
                if (boilTime >= 25) return; //if the boil time reaches a certain threshold, stops boiling and holds. 
                _boilCounter += Time.deltaTime; //adds to the boil counter by rate of time between frames.
                break;
            //otherwise;
            case false:
                _boilCounter -= Time.deltaTime; //lowers the boil counter by rate of time between frames. 
                break;
        }

        if (_boilCounter < 0) //if boil counter is less than 0
        {
            _boilCounter = 0; //resets boil counter to 0.
        }

        //boilTime = (int)(_boilCounter % 60); //converts boil counter into rounded whole number.
        boilTime = Mathf.FloorToInt(_boilCounter % 60);
    }
    
    //again this will change when we have boiling animations. 
    private void Steam()
    {
        //this is a ternary conditional operator, ultimately it returns a true or false bool, but we can put conditions in place of the bool as long as it adheres to "this or that"
        lowSteam.SetActive(boilTime > 5); //sets the low steam active based on the boil time.
        medSteam.SetActive(boilTime > 15); //sets the med steam active based on the boil time.

        switch (boilTime) 
        {
            case > 20: //if boiltime is greater than 20;
                highSteam.SetActive(true); //sets high steam to active.
                boilingWater.SetActive(true); //sets the boiling water animation to active.
                boiled = true; //marks the kettle as boiled.
                break;
            default: //otherwise;
                highSteam.SetActive(false); //high steam is inactive
                boilingWater.SetActive(false); //boiling water anim is inactive.
                boiled = false; //kettle is not boiled.
                break;
        }

    }
    private void OnTriggerEnter2D(Collider2D col) //when entering a trigger.
    {
        StartPouring(col.transform.tag); //Calls Start Pouring function and passes trigger object tag. 
        KettleOnBurner(col.transform.tag); //Calls kettle resetting function and passes trigger object tag.
    }
    
    private void StartPouring(string tag)
    {
        if (tag != "TeaPotLid") return; //if tag is not Tea Pot Lid; breaks.

        if (!boiled) return; //if the kettle is not boiled; breaks.
        StartCoroutine(MoveLid());
        StartCoroutine(KettlePour()); //pours the kettle.
    }
    private IEnumerator MoveLid()
    {
        teaLid.transform.DOMove(new Vector3(
            _lidX() + 1f,
            _lidY() + 1f,
            _lidZ()), 0.5f).SetEase(Ease.InOutCubic);
        teaLid.transform.DORotate(new Vector3(0, 0, -25),0.5f);
        yield break;
    }
    IEnumerator KettlePour()
    {
        transform.DORotate(new Vector3(0, 0, -25f), 0.5f).SetEase(Ease.InOutCubic);
        kettleLiquid.Play("water_pour", 0, 0f);
        yield return new WaitForSeconds(kettleLiquid.GetCurrentAnimatorClipInfo(0).Length);
        myManager.finishedPouring = true;
        //potSpriteChanger._filled = true;
    }

    #region Lid Tweening
    private void ResetLid()
    {
        teaLid.transform.DOMove(new Vector3( //tweens lid back to pot position. 
            _potX(),
            _potY(),
            _potZ()), 0.5f).SetEase(Ease.InOutCubic);
        teaLid.transform.DORotate(Vector3.zero, .5f); //rotates lid back to correct rotation.
        
    }
    #endregion

    #region On Trigger Enter Events

    #region Kettle Tweening


    IEnumerator KettleReset()
    {
        transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutCubic);
        yield break;
    }
    #endregion

    private void KettleOnBurner(string tag)
    {
        if (tag != "Burner")//if tag is not Burner;
        {
            return;
        }
        
        _onBurner = true; //Sets bool true.
    }
    #endregion

    private void OnTriggerStay2D(Collider2D other)
    {
        //StartPouring(other.transform.tag);
    }

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

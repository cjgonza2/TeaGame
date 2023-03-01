using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pot_SpriteChanger : MonoBehaviour
{
    [SerializeField] 
    public SpriteRenderer potSPR;

    [SerializeField]private GameManager myManager;
    [SerializeField] private Cam_Steep_Manager steepManager;
    
    [SerializeField] 
    private Sprite potWater;
    //different tea sprite combinations no water
    private Sprite _potBase;
    
    [Header("Pot Sprites")]
    [SerializeField] private Sprite potEmpty;
    [SerializeField] private Sprite baseNoIngDry;
    [SerializeField] private Sprite ingNoBaseDry;
    [SerializeField] private Sprite baseIngDry;
    [SerializeField] private Sprite baseNoIngFilled;
    [SerializeField] private Sprite ingNoBaseFilled;
    [SerializeField] private Sprite baseIngFilled;
    [SerializeField] private Sprite steepingTea;

    public Sprite mySprite;
    
    public Sprite currentSprite;

    [Header("Flavor Sprites")]
    //Reference to the flavor of tea sprites
    public Sprite potTea;
    public Sprite teaMed;
    public Sprite teaHigh;


    [SerializeField] 
    private Lid_Move _lid; //

    [SerializeField] 
    private Kettle_Move _kettle;

    public bool _filled = false;
    public bool _steeping = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        potSPR = GetComponent<SpriteRenderer>();
        steepManager = GetComponent<Cam_Steep_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFilled();
        PotFilledFirst();
        PotNotFilled();
        Steep();
        potSPR.sprite = mySprite;
    }

    private void Steep()
    {
        if (steepManager.teaBase && steepManager.teaIng && _filled)
        {
            mySprite = steepingTea;
        }
    }
    
    private void CheckIfFilled()
    {
        if (!myManager.finishedPouring) //Unless the manager is finished pouring;
        {
            return; //breaks the function
        }

        _filled = true; //sets the kettle to be filled.
    }
    
    private void PotFilledFirst()
    {
        if (!_filled)//unless the pot is not filled, 
        {
            return;
        }

        if (steepManager.teaBase) //if the there is a tea base in the pot;
        {
            mySprite = steepManager.teaIng ? baseIngFilled : baseNoIngFilled; //changes the sprite based on if there is an ingredient or not. 
        }
        
        if (!steepManager.teaBase)
        {
            mySprite = steepManager.teaIng ? ingNoBaseFilled : potWater;
        }
    }

    private void PotNotFilled()
    {
        if (_filled)
        {
            return;
        }

        if (steepManager.teaBase)
        {
            mySprite = steepManager.teaIng ? baseIngDry : baseNoIngDry;
        }
        
        if (!steepManager.teaBase)
        {
            mySprite = steepManager.teaIng ? ingNoBaseDry : potEmpty;
        }
    }
    
}

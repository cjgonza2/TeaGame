using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot_SpriteChanger : MonoBehaviour
{
    [SerializeField] 
    public SpriteRenderer potSPR;

    [SerializeField]private GameManager myManager;
    
    [SerializeField] 
    private Sprite potEmpty;
    [SerializeField] 
    private Sprite potWater;
    //different tea sprite combinations no water
    private Sprite _potBase;

    //Reference to the flavor of tea sprites
    public Sprite potTea;
    public Sprite teaMed;
    public Sprite teaHigh;

    public Sprite currentSprite;

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
    }

    // Update is called once per frame
    void Update()
    {
        FillWithWater();
        /*if (_kettle._pouring == false && _lid._colliding && _filled == false || _kettle._pouring && _lid._colliding && _filled ==false)
        {
            currentSprite = potEmpty;
            potSPR.sprite = currentSprite;
            //Debug.Log("You gotta remove the cover!");
        }*/

        if (_kettle._pouring && _lid._colliding == false)
        {
            currentSprite = potWater;
            potSPR.sprite = currentSprite;
            _filled = true;
        }

        if (_filled && _steeping)
        {
            //currentSprite = potTea;
            potSPR.sprite = currentSprite;
        }
    }

    private void FillWithWater()
    {
        if (!myManager.finishedPouring)
        {
            return;
        }
        currentSprite = potWater;
        potSPR.sprite = currentSprite;
    }
}

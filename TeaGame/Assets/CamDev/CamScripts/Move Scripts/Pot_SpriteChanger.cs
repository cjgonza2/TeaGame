using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot_SpriteChanger : MonoBehaviour
{
    [SerializeField] 
    public SpriteRenderer potSPR;
    
    [SerializeField] 
    private Sprite potEmpty;
    [SerializeField] 
    private Sprite potWater;
    [SerializeField] 
    private Sprite potTea;

    [SerializeField] 
    private Lid_Move _lid; //

    [SerializeField] 
    private Kettle_Move _kettle;

    private bool _filled = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
       potSPR = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (_kettle._pouring == false && _lid._colliding && _filled == false || _kettle._pouring && _lid._colliding && _filled ==false)
        {
            potSPR.sprite = potEmpty;
            Debug.Log("You gotta remove the cover!");
        }

        if (_kettle._pouring && _lid._colliding == false)
        {
            potSPR.sprite = potWater;
            _filled = true;
        }
    }
}

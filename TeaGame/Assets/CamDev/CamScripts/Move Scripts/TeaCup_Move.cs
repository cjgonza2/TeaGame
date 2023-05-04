using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TeaCup_Move : CamMove
{
    /*SerializeField] 
    private GameManager myManager;*/
    [SerializeField]
    private SpriteController cupSpr;

    [SerializeField] private GameObject pourCollider;

    public Vector3 sipPos;

    private Vector3 _startPos;
    
    private float cupY()
    {
        return gameObject.transform.position.y;
    }

    public override void Start()
    {
        base.Start();
        _startPos = gameObject.transform.position;
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override void Update()
    {
        base.Update();

        if (cupY() < -3 && !_selected)
        {
            if (myManager.currentState != GameManager.State.Resting)return;
            gameObject.transform.DOMoveY(_startPos.y, 0.5f);
        }

        if (cupY() > 4 && !_selected)
        {
            
            gameObject.transform.DOMoveY(0.5f, 0.5f);
        }
        
        if (cupSpr.fillCup)
        {
            //pourCollider.SetActive(false);
        }
    }
    private void OnMouseOver() //while the mouse is over;
    {
        if (_selected) //if the kettle is selected
        {
            highlight.SetActive(false); //sets the highlight object to inactive.
            return; //returns
        }
       
        //sets the highlight to active.
        highlight.SetActive(true);
    }

    private void OnMouseExit() //when the mouse moves away from the object;
    {
        highlight.SetActive(false); //deactivates highlight objects.
    }
    
    public override void OnMouseDown()
    {
        base.OnMouseDown();
        pourCollider.SetActive(false);
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        if (!cupSpr.fillCup)
        {
            pourCollider.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Character")) return;
        if (cupSpr.currentSprite != cupSpr.changedImage) return;
        _selected = false;
        gameObject.transform.position = sipPos;
        myManager.TransitionState(GameManager.State.Drinking);
        //Debug.Log("cheese;");
    }
}

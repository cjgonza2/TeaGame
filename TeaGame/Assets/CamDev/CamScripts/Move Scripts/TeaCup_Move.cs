using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TeaCup_Move : CamMove
{
    [SerializeField] 
    private GameManager manager;
    [SerializeField]
    private SpriteController cupSpr;

    [SerializeField] private GameObject pourCollider;

    public Vector3 sipPos;

    public override void Start()
    {
        base.Start();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override void Update()
    {
        base.Update();
        if (cupSpr.fillCup)
        {
            pourCollider.SetActive(false);
        }
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        pourCollider.SetActive(false);
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp();
        pourCollider.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Character")) return;
        if (cupSpr.currentSprite != cupSpr.changedImage) return;
        _selected = false;
        gameObject.transform.position = sipPos;
        manager.TransitionState(GameManager.State.Drinking);
        Debug.Log("cheese;");
    }
}

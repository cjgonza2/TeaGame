using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;

public class TeaClump : MonoBehaviour
{
    [SerializeField] 
    private Cam_Steep_Manager potSteep; //lets us pass the flavor to steep manager.

    private TeaLeavesController leaveController;

    private CamMove _moveScript;

    private bool _holdingClump = false;
    
    public GameObject lid;

    private float _clumpX()
    {
        return transform.position.x;
    }

    private float _clumpY()
    {
        return transform.position.y;
    }
    private float _lidX()
    {
        return lid.transform.position.x;
    }
    private float _lidY()
    {
        return lid.transform.position.y;
    }
    private float _lidZ()
    {
        return lid.transform.position.z;
    }

    private bool _holdLid = false;

    #region TeaPot GameObject/Coordinates
    public GameObject teaPot;
    private float _potXMax()
    {
        return (teaPot.transform.position.x + 1.6f);
    }
    private float _potXMin()
    {
        return (teaPot.transform.position.x - 1.6f);
    }

    private float _potX()
    {
        return teaPot.transform.position.x;
    }
    private float _potY()
    {
        return teaPot.transform.position.y;
    }
    private float _potZ()
    {
        return teaPot.transform.position.z;
    }
    #endregion

    
    public Vector2 restPos;
    public Vector2 inPotPos;

    private Sequence _lidSequence;
    private Sequence _lidCloseSequence;
    
    private void Awake()
    {
        //Debug.Log(gameObject.tag);
        _lidSequence = DOTween.Sequence();
        _lidCloseSequence = DOTween.Sequence();
        _holdingClump = true;
        restPos = new Vector2(0, -7.5f);
        teaPot = GameObject.Find("TeaPot");
        lid = GameObject.Find("TeaPotLid");
        potSteep = teaPot.GetComponent<Cam_Steep_Manager>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y < restPos.y)
        {
            Destroy(gameObject);
        }
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        TeaPotCollision(col.transform.tag);
    }

    private void TeaPotCollision(string tag)
    {
        if (tag != "TeaPotLid")
        {
            return;
        }
        SetBase(transform.tag);
        SetIngredient(transform.tag);
        transform.position = restPos;
    }
    
    private void SetBase(string teaBase)
    {
        if (potSteep.teaBase)
        {
            return;
        }
        
        switch (teaBase)
        {
            case "bitter":
                potSteep.bitter = true;
                potSteep.teaBase = true;
                break;
            case "mild":
                potSteep.mild = true;
                potSteep.teaBase = true;
                break;
            case "sweet":
                potSteep.sweet = true;
                potSteep.teaBase = true;
                break;

        }
    }

    private void SetIngredient(string teaIng)
    {
        if (potSteep.teaIng)
        {
            return;
        }
        switch (teaIng)
        {
            case "sleep":
                Debug.Log("sleep");
                potSteep.sleep = true;
                potSteep.teaIng = true;
                break;
            case "energy":
                Debug.Log("energy");
                potSteep.energy = true;
                potSteep.teaIng = true;
                break;
            case "health":
                Debug.Log("health");
                potSteep.health = true;
                potSteep.teaIng = true;
                break;
            default:
                Debug.Log("no ingredient detected.");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        MoveLid(col.transform.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (!_holdingClump)
        {
            return;
        }*/
        
        ResetLid();
    }

    private void MoveLid(string collidertag)
    {
        if (collidertag != "TeaPotLid") return;
        if (_lidSequence.IsPlaying()) return;
        _lidSequence.Append(lid.transform.DOMove(new Vector3( //tweens lid up to the right. 
            _lidX() + 1f,
            _lidY() + 1f,
            _lidZ()), 0.5f).SetEase(Ease.InOutCubic));
        _lidSequence.Join(lid.transform.DORotate(new Vector3(0, 0, -25), .5f)); //rotates lid. 
        _lidSequence.Play(); 
    }

    private void ResetLid()
    {
        if (_lidCloseSequence.IsPlaying()) return;
        _lidCloseSequence.Append(lid.transform.DOMove(new Vector3(
            _potX(),
            _potY(),
            _potZ()), 0.5f).SetEase(Ease.InOutCubic));
        _lidCloseSequence.Append(lid.transform.DORotate(Vector3.zero, .5f));
        _lidCloseSequence.Play();
    }
}

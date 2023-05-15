using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeaPot_Move : CamMove
{
    [SerializeField] private GameObject lidHighlight;
    
    [Header("Managers")]
    //[SerializeField] private GameManager myManager;
    [SerializeField] private PouringManager pourManager;
    [SerializeField] private Cam_Steep_Manager _steepManager;
    [SerializeField] private Pot_SpriteChanger potSprite;

    [SerializeField] private AudioSource pourWater;
    [Header("Sprite Changer")]
    [SerializeField] 
    private Pot_SpriteChanger sprTrack;
    [SerializeField] private SpriteController cupSprite;

    [Header("Animator")] 
    [SerializeField] private Animator teaPourAnim;
    [Header("TeaPot Lid")]
    [SerializeField]private GameObject teaPotLid;
    [SerializeField] private GameObject lidPouringCollider;
    [SerializeField] private GameObject teaCupCollider;
    private bool _lidMoved = false;


    // pot/lid position coordinates.
    #region Lid Transform Position
    private float _teaLidXPos()
    {
        return teaPotLid.transform.position.x;
    }

    private float _teaLidYPos()
    {
        return teaPotLid.transform.position.y;
    }

    private float _teaLidZPos()
    {
        return teaPotLid.transform.position.z;
    }
    #endregion

    #region Pot Transform Position
    [SerializeField]private Vector3 startPos;
    private float _PotX()
    {
        return transform.position.x;
    }

    private float _PotY()
    {
        return transform.position.y;
    }

    private float _PotZ()
    {
        return transform.position.z;
    }
    #endregion


    public override void Start()
    {
        base.Start(); //does everything parent function does. 
        startPos = gameObject.transform.position;
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //assigns game manager reference.
        teaPotLid = GameObject.Find("teapot_lid"); //assigns teapot lid reference;
        //_myManager = PouringManager.FindInstance();
    }

    public override void Update()
    {
        base.Update(); //Does everything parent script does.

        if (_PotY() < -3 && !_selected)
        {
            gameObject.transform.DOMoveY(startPos.y, 0.5f);
        }

        if (_PotY() > 4 && !_selected)
        {
            gameObject.transform.DOMoveY(0.5f, 0.5f);
        }
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown(); //Does everything parent function does. 
        //pourCollider.enabled = false; //disables pouring collider. 
        lidPouringCollider.SetActive(false);
        if (sprTrack._steeping && _steepManager._finishedSteep == false)
        {
            //Debug.Log("you've clicekd me");
            //_steepManager._finishedSteep = true;
        }
    }

    public override void OnMouseUp()
    {
        base.OnMouseUp(); //Does everything parent function does.
        if (!potSprite._steeping)
        {
            lidPouringCollider.SetActive(true);
        }
        //pourCollider.enabled = true; //enables pouring collider. 
    }
    private void OnMouseOver() //while the mouse is over;
    {
        if (_selected) //if the kettle is selected
        {
            highlight.SetActive(false); //sets the highlight object to inactive.
            lidHighlight.SetActive(false);
            return; //returns
        }
       
        //sets the highlight to active.
        highlight.SetActive(true);
        lidHighlight.SetActive(true);
    }

    private void OnMouseExit() //when the mouse moves away from the object;
    {
        highlight.SetActive(false); //deactivates highlight objects.
        lidHighlight.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("cup")) return; //unless the pot is colliding with the cup, breaks.

        if (!sprTrack._steeping) return;

        pourWater.Play();
        transform.DORotate(new Vector3(0, 0, 25f), 0.5f).SetEase(Ease.InOutCubic);
        teaPourAnim.Play("pour_animation"); 
        _steepManager._finishedSteep = true;
        cupSprite.fillCup = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("cup")) return;
        if (sprTrack._steeping)
        {
            pourWater.Stop();
            transform.DORotate(new Vector3(0, 0, 0f), 0.5f).SetEase(Ease.InOutCubic);
        }
        teaCupCollider.SetActive(false);
    }

    #region TweenTesting

    //private bool _spaceinput;
    IEnumerator MoveLid()
    {
        Debug.Log(_lidMoved);
        teaPotLid.transform.DOMove(new Vector3(
            _teaLidXPos() + 0.75f,
            _teaLidYPos() + 0.75f,
            _teaLidZPos()), 1f).SetEase(Ease.InOutCubic);
        teaPotLid.transform.DORotate(new Vector3(0, 0, -25), 1f);
        yield return new WaitForSeconds(0.1f);
        //myManager.lidMoved = true;
        
        //_spaceinput = true;
    }
    IEnumerator ResetLidTest()
    {
        yield return new WaitForSeconds(0.1f);
        teaPotLid.transform.DOMove(new Vector3(
            _PotX(),
            _PotY(),
            _PotZ()), 1f).SetEase(Ease.InOutCubic);
        teaPotLid.transform.DORotate(new Vector3(0, 0, 0), 1f);

    }


    #endregion
}

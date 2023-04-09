using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChracterManager : MonoBehaviour
{
    public GameManager manager;
    [SerializeField] private CycleManager cycleManager;
    public Cam_Steep_Manager steepManager;

    [SerializeField]
    private string locale;
    private string _currentCharacter;
    
    [SerializeField] private GameObject character;
    
    #region Lombardo
    [Header("Lombardo")]

    [Header("Lombardo Sprite Render")]
    [SerializeField] private SpriteRenderer lambSpr;
    [Header("Sprites")]
    [SerializeField] private Sprite lambIdle;
    [SerializeField] private Sprite lambSip;
    [SerializeField] private Sprite lambReact;
    private Sprite _lambCurrentSprite;
    
    [SerializeField] private bool lambPresent = false;
    #endregion

    #region Rana
    [Header("Rana")] 
    
    [Header("Rana Sprite Renderer")] //Rana's Sprite Renderer
    [SerializeField] private SpriteRenderer ranaSpr;
    [Header("Rana Sprites")] //reaction sprite references
    [SerializeField] private Sprite ranaIdle;
    [SerializeField] private Sprite ranaSip;
    [SerializeField] private Sprite ranaSmile;
    [SerializeField] private Sprite ranaReact;
    private Sprite _ranaCurrentSprite; //variable for the current sprite being used for character. 
    
    [SerializeField] private bool ranaPresent; //bool tracking if the character is on screen.
    #endregion

    #region Shi'Wi
    [Header("Shi'Wi")] 
    
    [Header("Shi'Wi Sprite Renderer")] 
    [SerializeField] private SpriteRenderer shiSpr;
    [Header("Shi'Wi Sprites")]
    [SerializeField] private Sprite shiIdle;
    [SerializeField] private Sprite shiSip;
    [SerializeField] private Sprite shiSmile;
    [SerializeField] private Sprite shiReact;
    private Sprite _shiWiCurrentSprite;

    [SerializeField] private bool shiPresent;
    #endregion
    
    
    [Header("Tween Positions")] //posiitons for character entrance and exits.
    [SerializeField] private Vector3 entrancePos;
    [SerializeField] private Vector3 exitPos;
    
    public bool _sceneEnd = false;


    private void Start()
    {
        cycleManager = CycleManager.FindInstance(); //finds cycle manager instance. 
    }

    #region Character Starts
    private IEnumerator LombardoWaitToStart()
    {
        yield return new WaitForSeconds(0.2f);  //waits for a split second before entering. 
        character.transform.DOMove(entrancePos, 0.5f).SetEase(Ease.InOutCubic); //tweens character to start position.
        lambPresent = true; //sets character to present. 
    }

    private IEnumerator RanaWaitToStart()
    {
        yield return new WaitForSeconds(0.2f);
        character.transform.DOMove(entrancePos, 0.5f).SetEase(Ease.InOutCubic);
        ranaPresent = true;
    }

    private IEnumerator ShiWiWaitToStart()
    {
        yield return new WaitForSeconds(0.2f);
        character.transform.DOMove(entrancePos, 0.5f).SetEase(Ease.InOutCubic);
        shiPresent = true;
    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        //if the game's current state is start;
        if (manager.currentState == GameManager.State.Enter)
        {
            StartCoroutine(SetLocale(manager._currentScene));
            // SetCharacter();
        }
        Drinking();
        Tasting();
        Exiting();

    }
    
    IEnumerator SetLocale(string scene)
    {
        locale = scene; //sets locale to the current scene. 
        SetCharacter(); //sets the character based on current scene.
        manager.TransitionState(GameManager.State.Resting); //changes the state to resting. 
        yield break;
    }

    private void SetCharacter()
    {
        GoyoCanyon();
        LilyBog();
        OotalCliffs();

        StartCoroutine($"{_currentCharacter}WaitToStart");
    }

    #region LocationSetting
    private void GoyoCanyon()
    {
        //checks if we're in the right location.
        if (locale != "Goyo_Canyon") return;

        _sceneEnd = false;
        _currentCharacter = "Lombardo";
        steepManager = GameObject.Find("TeaPot").GetComponent<Cam_Steep_Manager>();
        character = GameObject.Find("Lombardo");
        lambSpr = character.GetComponent<SpriteRenderer>();
    }

    private void LilyBog()
    {
        //if locale is not lily bog:
        if (locale != "Lily_Bog") return;

        //if location is lily bog:
        _sceneEnd = false;
        _currentCharacter = "Rana"; //sets the current character
        character = GameObject.Find("Rana"); //finds the character game object
        steepManager = GameObject.Find("TeaPot").GetComponent<Cam_Steep_Manager>();
        ranaSpr = character.GetComponent<SpriteRenderer>();
    }

    private void OotalCliffs()
    {
        if (locale != "Ootal_Cliffs")
        {
            return;
        }

        _sceneEnd = false;
        _currentCharacter = "ShiWi";
        character = GameObject.Find("ShiWi");
        steepManager = GameObject.Find("TeaPot").GetComponent<Cam_Steep_Manager>();
        shiSpr = character.GetComponent<SpriteRenderer>();
    }
    #endregion
    
    private void Drinking()
    {
        if (manager.currentState != GameManager.State.Drinking)
        {
            return;
        }
        LombardoDrink();
        RanaDrink();
        ShiWiDrink();
    }

    private void LombardoDrink()
    {
        //checks if we have the right character or not.
        if (_currentCharacter != "Lombardo")return; 
        _lambCurrentSprite = lambSip; //sets currentsprite to sip sprite
        lambSpr.sprite = _lambCurrentSprite; //sets sprite renderer to new current sprite. 
    }

    private void RanaDrink()
    {
        if (_currentCharacter != "Rana")return;
        _ranaCurrentSprite = ranaSip;
        ranaSpr.sprite = _ranaCurrentSprite;
    }

    private void ShiWiDrink()
    {
        if (_currentCharacter != "ShiWi") return;
        _shiWiCurrentSprite = shiSip;
        shiSpr.sprite = _shiWiCurrentSprite;
    }
    
    void Tasting()
    {
        if (manager.currentState != GameManager.State.Tasting) return;
        
        LombardoTasting();
        RanaTasting();
        ShiWiTasting();
    }

    private void LombardoTasting()
    {
        if (!lambPresent) return;

        if (steepManager.sweetSleep)
        {
            //if the tea blend is made of bitter and health.
            if (steepManager.highFlavor)
            {
                //sets character sprite to idle.
                _lambCurrentSprite = lambIdle;
                lambSpr.sprite = _lambCurrentSprite;
            }
            else
            {
                //sets char sprite to negative react sprite. 
                _lambCurrentSprite = lambReact;
                lambSpr.sprite = _lambCurrentSprite;
            }
        }
        else
        {
            //sets char sprite to negative react sprite. 
            _lambCurrentSprite = lambReact;
            lambSpr.sprite = _lambCurrentSprite;
        }
    }

    private void RanaTasting()
    {
        if(!ranaPresent)return;
        if (steepManager.bitterHealth) //if the tea flavor is bitter health
        {
            if (steepManager.lowFlavor)
            {
                _ranaCurrentSprite = ranaSmile;
                ranaSpr.sprite = _ranaCurrentSprite;
            }
            else
            {
                _ranaCurrentSprite = ranaReact;
                ranaSpr.sprite = _ranaCurrentSprite;
            }
        }
        else
        {
            _ranaCurrentSprite = ranaReact;
            ranaSpr.sprite = _ranaCurrentSprite;
        }
        //decide what flavor of tea she likes w/ tassneen
    }

    private void ShiWiTasting()
    {
        if (!shiPresent) return;
        if (steepManager.mildEnergy)
        {
            if (steepManager.lowFlavor)
            {
                _shiWiCurrentSprite = shiSmile;
                shiSpr.sprite = _shiWiCurrentSprite;
            }
            else
            {
                _shiWiCurrentSprite = shiReact;
                shiSpr.sprite = _shiWiCurrentSprite;
            }
        }
        else
        {
            _shiWiCurrentSprite = shiReact;
            shiSpr.sprite = _shiWiCurrentSprite;
        }
    }

    #region Exiting
   private void Exiting()
    {
        LombardoExit();
        RanaExit();
        ShiWiExit();
        if (!_sceneEnd) return;
        cycleManager.NextScene();
    }

    private void LombardoExit()
    {
        if (manager.currentState != GameManager.State.Exiting) return;

        if (!lambPresent) return;
        
        StartCoroutine(LomWaitToExit());
    }

    private void RanaExit()
    {
        if (manager.currentState != GameManager.State.Exiting) return;
        
        if (!ranaPresent) return;
        
        StartCoroutine(RanaWaitToExit());
    }

    private void ShiWiExit()
    {
        if (manager.currentState != GameManager.State.Exiting) return;
        if (!shiPresent) return;
        StartCoroutine(ShiWiWaitToExit());

    }
    
    private IEnumerator LomWaitToExit() //Lombardo's exit function.
    {
        lambPresent = false;
        //Debug.Log("about to start");
        character.transform.DOMove(exitPos, 0.5f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(1);
        _sceneEnd = true;
    }

    private IEnumerator RanaWaitToExit() //rana's exit funciton
    {
        ranaPresent = false;
        character.transform.DOMove(exitPos, 0.5f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(1);
        _sceneEnd = true;
    }
    #endregion

    private IEnumerator ShiWiWaitToExit()
    {
        shiPresent = false;
        character.transform.DOMove(exitPos, 0.5f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(1);
        _sceneEnd = true;
    }
}

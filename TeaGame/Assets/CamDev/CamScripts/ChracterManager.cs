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
    /*[Header("Animator")]
    [SerializeField] 
    private Animator lambAnim;*/
    [Header("Lombardo Sprite Render")]
    [SerializeField] 
    private SpriteRenderer lambSpr;
    [Header("Sprites")]
    [SerializeField] 
    private Sprite lambIdle;
    [SerializeField] 
    private Sprite lambSip;
    [SerializeField] 
    private Sprite lambReact;
    private Sprite _lambCurrentSprite;
    [SerializeField]
    private bool lambPresent = false;
    #endregion

    #region Rana
    [Header("Rana")] 
    
    [Header("Rana Sprite Renderer")]
    [SerializeField] private SpriteRenderer ranaSpr;
    [Header("Rana Sprites")]
    [SerializeField] private Sprite ranaIdle;
    [SerializeField] private Sprite ranaSip;
    [SerializeField] private Sprite ranaSmile;
    [SerializeField] private Sprite ranaReact;
    private Sprite _ranaCurrentSprite;
    
    [SerializeField] private bool ranaPresent;
    #endregion

    [Header("Tween Positions")]
    [SerializeField] private Vector3 entrancePos;
    [SerializeField] private Vector3 exitPos;
    
    public bool _sceneEnd = false;

    IEnumerator SetLocale(string scene)
    {
        locale = manager._currentScene; //sets locale to the current scene. 
        SetCharacter(); //sets the character based on current scene.
        manager.TransitionState(GameManager.State.Resting); //changes the state to resting. 
        yield break;
    }
    private IEnumerator LombardoWaitToStart()
    {
        //Debug.Log("coroutine was called");
        yield return new WaitForSeconds(0.2f);  //waits for a split second before entering. 
        character.transform.DOMove(entrancePos, 0.5f).SetEase(Ease.InOutCubic);
        //lambAnim.Play("LombardoEnter", 0,0f); //plays lombardo enter animation.
        lambPresent = true; //sets lombardo to present. 
    }

    private IEnumerator RanaWaitToStart()
    {
        yield return new WaitForSeconds(0.2f);
        character.transform.DOMove(entrancePos, 0.5f).SetEase(Ease.InOutCubic);
        ranaPresent = true;
    }


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

    private void SetCharacter()
    {
        GoyoCanyon();
        LilyBog();
        ShangoliRiverlands();
        UtalCliffs();

        StartCoroutine($"{_currentCharacter}WaitToStart");
    }

    #region LocationSetting
    private void GoyoCanyon()
    {
        if (locale != "Goyo_Canyon") return;

        _sceneEnd = false;
        _currentCharacter = "Lombardo";
        steepManager = GameObject.Find("TeaPot").GetComponent<Cam_Steep_Manager>();
        character = GameObject.Find("Lombardo");
        lambSpr = character.GetComponent<SpriteRenderer>();
    }

    private void LilyBog()
    {
        if (locale != "Lily_Bog")
        {
            return;
        }

        _sceneEnd = false;
        _currentCharacter = "Rana";
        character = GameObject.Find("Rana");
        steepManager = GameObject.Find("TeaPot").GetComponent<Cam_Steep_Manager>();
        character = GameObject.Find("Rana");
        ranaSpr = character.GetComponent<SpriteRenderer>();
    }

    private void ShangoliRiverlands()
    {
        if (locale != "Shangoli_Riverlands")
        {
            return;
        }

        _currentCharacter = "";
    }

    private void UtalCliffs()
    {
        if (locale != "Utal_Cliffs")
        {
            return;
        }

        _currentCharacter = "";
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
    }

    private void LombardoDrink()
    {
        if (_currentCharacter != "Lombardo")return; //if the current character is not lombardo; breaks. 
        _lambCurrentSprite = lambSip;
        lambSpr.sprite = _lambCurrentSprite;
    }

    private void RanaDrink()
    {
        if (_currentCharacter != "Rana")return;
        _ranaCurrentSprite = ranaSip;
        ranaSpr.sprite = _ranaCurrentSprite;
    }

    void Tasting()
    {
        if (manager.currentState != GameManager.State.Tasting) return;
        
        LombardoTasting();
        RanaTasting();
        
        /*
        if (manager.currentState == GameManager.State.Tasting)
        {
            //if the tea blend is made of bitter and health.
            if (steepManager.bitterHealth)
            {
                //and if the flavor is high.
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
            else //otherwise;
            {
                //sets char sprite to negative react sprite. 
                _lambCurrentSprite = lambReact;
                lambSpr.sprite = _lambCurrentSprite;
            }
        }*/
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
        if (steepManager.bitterHealth)
        {
            if (steepManager.lowFlavor)
            {
                _ranaCurrentSprite = ranaSmile;
                ranaSpr.sprite = _lambCurrentSprite;
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

    #region Exiting
   private void Exiting()
    {
        LombardoExit();
        RanaExit();
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
        if (!lambPresent) return;
        StartCoroutine(RanaWaitToExit());
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
    
}

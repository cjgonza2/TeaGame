using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChracterManager : MonoBehaviour
{
    public GameManager manager;
    public Cam_Steep_Manager steepManager;

    [SerializeField]
    private string locale;
    private string _currentCharacter;

    #region Lombardo
    [Header("Lombardo")]
    [Header("Animator")]
    [SerializeField] 
    private Animator lambAnim;
    [Header("Sprite Render")]
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

    #endregion

    public bool _sceneEnd = false;

    IEnumerator SetLocale(string scene)
    {
        locale = manager._currentScene; //sets locale to the current scene. 
        SetCharacter(); //sets the character based on current scene.
        manager.TransitionState(GameManager.State.Resting); //changes the state to resting. 
        yield break;
    }
    IEnumerator LombardoWaitToStart()
    {
        Debug.Log("coroutine was called");
        yield return new WaitForSeconds(0.2f);  //waits for a split second before entering. 
        lambAnim.Play("LombardoEnter", 0,0f); //plays lombardo enter animation.
        lambPresent = true; //sets lombardo to present. 
    }

    IEnumerator LomWaitToExit()
    {
        lambPresent = false;
        Debug.Log("about to start");
        yield return new WaitForSeconds(0.1f);
        lambAnim.Play("LambardoExit", 0, 0f);
        _sceneEnd = true;
    }
    
    void Start()
    {
        
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

    void SetCharacter()
    {
        if (locale == "Goyo_Canyon")
        {
            _currentCharacter = "Lombardo";
        }
        StartCoroutine($"{_currentCharacter}WaitToStart");
    }
    
    void Drinking()
    {
        if (manager.currentState == GameManager.State.Drinking)
        {
            if (_currentCharacter == "Lombardo")
            {
                _lambCurrentSprite = lambSip;
                lambSpr.sprite = _lambCurrentSprite;
            }
        }
    }

    void Tasting()
    {
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
        }
    }

    void Exiting()
    {
        if (manager.currentState == GameManager.State.Exiting && lambPresent)
        {
            Debug.Log("leave me");
            StartCoroutine(LomWaitToExit());
            //lambAnim.Play("LambardoExit", 0, 0);
        }
    }
}

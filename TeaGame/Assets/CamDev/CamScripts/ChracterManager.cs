using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChracterManager : MonoBehaviour
{
    public GameManager manager;
    public Cam_Steep_Manager steepManager;

    #region Lombardo
    [Header("Lambardo")]
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
    
    // Start is called before the first frame update
    IEnumerator WaitToStart()
    {
        Debug.Log("coroutine was called");
        yield return new WaitForSeconds(0.2f);
        lambAnim.Play("LombardoEnter", 0,0f);
        lambPresent = true;
    }

    IEnumerator WaitToExit()
    {
        lambPresent = false;
        Debug.Log("about to start");
        yield return new WaitForSeconds(0.1f);
        lambAnim.Play("LambardoExit", 0, 0f);
        _sceneEnd = true;
    }
    
    void Start()
    {
        if (manager.currentState == GameManager.State.Enter)
        {
            Debug.Log("coroutine can be called");
            StartCoroutine(WaitToStart());
        }
    }

    // Update is called once per frame
    void Update()
    {

        Drinking();
        Tasting();
        Exiting();

    }

    void Drinking()
    {
        if (manager.currentState == GameManager.State.Drinking)
        {
            _lambCurrentSprite = lambSip;
            lambSpr.sprite = _lambCurrentSprite;
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
            StartCoroutine(WaitToExit());
            //lambAnim.Play("LambardoExit", 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Pot_SpriteChanger : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField]private GameManager myManager;
    [SerializeField] private Cam_Steep_Manager steepManager;
    
    [Header("Sprite Renderers")]
    public SpriteRenderer mainPotSpr;
    public Color mainColor;
    public SpriteRenderer lowSteepSpr;
    public Color lowColor;
    public SpriteRenderer highSteepSpr;
    public Color highColor;

    [Header("Pot Sprites")]
    [SerializeField] private Sprite potEmpty;
    [SerializeField] private Sprite potFilled;

    [Header("Dry Sprites")]
    [SerializeField] private List<Sprite> dryBaseSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> dryIngSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> dryTisaneSprites = new List<Sprite>();
    [Header("Filled Sprites")]
    [SerializeField] private List<Sprite> filledBaseSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledIngSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledTisaneSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledLowSteepSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> filledHighSteepSprites = new List<Sprite>();

    [Header("Current Sprite")]
    public Sprite mySprite;

    private Sprite _highSprite;
    private Sprite _lowSprite;

        [Header("Bools")]
    public bool _filled = false;
    public bool _steeping = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainPotSpr = GetComponent<SpriteRenderer>();
        steepManager = GetComponent<Cam_Steep_Manager>();
        mySprite = mainPotSpr.sprite;
    }

    // Update is called once per frame
    private void Update()
    {
        PotNotFilled();
        CheckIfFilled();
        PotFilledFirst();
        TeaCompCheck();
        Steep();
        mainPotSpr.sprite = mySprite;
        lowSteepSpr.sprite = _lowSprite;
        highSteepSpr.sprite = _highSprite;
    }
    
    private void PotNotFilled() //if the pot is not filled with water.
    {
        if (_filled) //returns if pot is already filled.
        {
            return;
        }

        if (!steepManager.teaBase && !steepManager.teaIng)
        {
            mySprite = potEmpty;
        }
    }
    private void CheckIfFilled()
    {
        if (!myManager.finishedPouring) return; //Unless the manager is finished pouring;

        _filled = true; //sets the kettle to be filled.
    }
    private void PotFilledFirst()
    {
        if (!_filled) return; //unless the pot is not filled, 
        
        mySprite = potFilled;
        
    }

    #region Fill Checks
    private void TeaCompCheck()
    {
        switch (steepManager.teaBase)
        {
            case true:
                MildCheck();
                SweetCheck();
                BitterCheck();
                break;
            case false:
                SleepCheck();
                EnergyCheck();
                HealthCheck();
                break;
        }
    }

    #region Ingredient Only Checks
    private void SleepCheck()
    {
        if (!steepManager.sleep) return;
        
        DryFilledIngChange(0);
    }
    private void EnergyCheck()
    {
        if (!steepManager.energy) return;

        DryFilledIngChange(1);
    }
    private void HealthCheck()
    {
        if (!steepManager.health) return;
        
        DryFilledIngChange(2);
    }
    #endregion
    
    private void MildCheck()
    {
        if (!steepManager.mild) return;

        switch (steepManager.teaIng)
        {
            case true:
                MildSleep();
                MildEnergy();
                MildHealth();
                break;
            case false:
                DryFilledBaseChange(0);
                break;
        }
    }

    #region Mild Tisanes
    private void MildSleep()
    {
        if (!steepManager.sleep) return;

        DryFilledTisaneChange(0);
    }

    private void MildEnergy()
    {
        if (!steepManager.energy) return;
        
        DryFilledTisaneChange(1);
    }
    private void MildHealth()
    {
        if (!steepManager.health) return;

        DryFilledTisaneChange(2);
    }
    #endregion
    
    private void SweetCheck()
    {
        if (!steepManager.sweet) return;

        switch (steepManager.teaIng)
        {
            case true:
                SweetSleep();
                SweetEnergy();
                SweetHealth();
                break;
            case false:
                DryFilledBaseChange(1);
                break;
        }
    }

    #region Sweet Tisanes
    private void SweetSleep()
    {
        if (!steepManager.sweet) return;
        
        DryFilledTisaneChange(3);
    }
    private void SweetEnergy()
    {
        if (!steepManager.energy) return;

        DryFilledTisaneChange(4);
    }
    private void SweetHealth()
    {
        if (!steepManager.health) return;

        DryFilledTisaneChange(5);
    }
    #endregion
    
    private void BitterCheck()
    {
        if (!steepManager.bitter) return;

        switch (steepManager.teaIng)
        {
            case true:
                BitterSleep();
                BitterEnergy();
                BitterHealth();
                break;
            case false:
                DryFilledBaseChange(2);
                break;
        }
    }

    #region Bitter Tisanes
    private void BitterSleep()
    {
        if (!steepManager.sleep) return;

        DryFilledTisaneChange(6);
    }
    private void BitterEnergy()
    {
        if (!steepManager.energy) return;

        DryFilledTisaneChange(7);
    }
    private void BitterHealth()
    {
        if (!steepManager.health) return;

        DryFilledTisaneChange(8);
    }
    #endregion
    
    //the Dry filled checks corrispond to what components are already in the teapot
    //then it just checks based on whether the pot is filled.
    private void DryFilledTisaneChange(int x)
    {
        mySprite = _filled switch
        {
            true => filledTisaneSprites[x],
            false => dryTisaneSprites[x]
        };

        _lowSprite = _filled switch
        {
            true => filledLowSteepSprites[x],
            false => null
        };
        _highSprite = _filled switch
        {
            true => filledHighSteepSprites[x],
            false => null
        };
    }
    private void DryFilledBaseChange(int x)
    {
        mySprite = _filled switch
        {
            true => filledBaseSprites[x],
            false => dryBaseSprites[x]
        };
    }
    private void DryFilledIngChange(int x)
    {
 
        
        mySprite = _filled switch
        {
            true => filledIngSprites[x],
            false => dryIngSprites[x]
        };
        
        
    }
    #endregion


    private void Steep()
    {
        if (!steepManager.teaBase) return;
        if (!steepManager.teaIng) return;
        if (!_filled) return;
        StartCoroutine(FadeToLowSteep());
        if (steepManager._finishedSteep) return;
        if (steepManager.roundedTime < 8) return;
        StartCoroutine(FadeToHighSteepOne());


    }
    private IEnumerator FadeToLowSteep()
    {
        if (steepManager.roundedTime <= 2)
        {
            mainPotSpr.DOFade(0, 3);
        }
        yield break;
    }

    private IEnumerator FadeToHighSteepOne()
    {
        Debug.Log("fade to high started.");
        lowSteepSpr.DOFade(0, 11f);
        yield break;

    }
    

    
}

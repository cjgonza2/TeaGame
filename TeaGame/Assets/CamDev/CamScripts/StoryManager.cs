using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using Unity.VisualScripting;

public class StoryManager : MonoBehaviour
{
    [Header("Ink")]
    public TextAsset inkAsset; //reference to the Ink Json File.
    private Story _inkStory;
    
    [Header("cycleManager")]
    [SerializeField] private CycleManager cycleManager;

    [Header("Animators")]
    [Header("Vignette Two")]
    [SerializeField] private Animator vignetteTwo_Animator;
    [Header("Vignette Three")]
    [SerializeField] private Animator vignetteThree_Animator;
    [Header("Line One")]
    [SerializeField] private Animator lineOne_Animator;
    [Header("Line Two")]
    [SerializeField] private Animator lineTwo_Animator;
    [Header("Line Three")]
    [SerializeField] private Animator lineThree_Animator;
    [Header("Line Four")]
    [SerializeField] private Animator lineFour_Animator;
    [Header("Line Five")]
    [SerializeField] private Animator lineFive_Animator;
    [Header("Line Six")]
    [SerializeField] private Animator lineSix_Animator;
    [Header("Line Seven")]
    [SerializeField] private Animator lineSeven_Animator;
    [Header("Line Eight")]
    [SerializeField] private Animator lineEight_Animator;
    [Header("Line Nine")]
    [SerializeField] private Animator lineNine_Animator;
    [Header("Line Ten")]
    [SerializeField] private Animator lineTen_Animator;
    [Header("Line Eleven")]
    [SerializeField] private Animator lineEleven_Animator;
    [Header("Line Twelve")]
    [SerializeField] private Animator lineTwelve_Animator;
    [Header("Line Thirteen")]
    [SerializeField] private Animator lineThirteen_Animator;
    [Header("Line Fourteen")]
    [SerializeField] private Animator lineFourteen_Animator;
    [Header("Panel")] 
    [SerializeField] private Animator SceneFade;

    [Header("Text")]
    [SerializeField] private List<TMP_Text> _text;

    private List<string> _lines;


    private int _lineNumb;
    private bool _skip;
    private bool _lineOne;
    private bool _lineTwo;
    private bool _lineThree;
    private bool _lineFour;
    private bool _batchFadeOne;
    private bool _lineFive;
    private bool _lineSix;
    private bool _lineSeven;
    private bool _lineEight;
    private bool _batchFadeTwo;
    private bool _lineNine;
    private bool _lineTen;
    private bool _lineEleven;
    private bool _lineTwelve;
    private bool _lineThirteen;
    private bool _lineFourteen;

    //YOU CAN NOT USE START/UPDATE IN THE SCRIPT THAT'S PARSING THE INK FILE.
    private void Awake()
    {
        _inkStory = new Story(inkAsset.text);
        _lines = new List<string>();
        //cycleManager = CycleManager.FindInstance();
        StartCoroutine(InitialLines());
    }

    private void Update()
    {
        print(_lineNumb);
        if (!Input.GetMouseButtonDown(0)) return;
        _lineNumb++;
        switch (_lineNumb)
        {
            case 1:
                _lineOne = true;
                StartCoroutine(FadeInLineOne());
                break;
            case 2:
                _lineTwo = true;
                StartCoroutine(FadeInLineTwo());
                break;
            case 3:
                _lineThree = true;
                StartCoroutine(FadeInLineThree());
                break;
            case 4:
                _lineFour = true;
                StartCoroutine(FadeInLineFour());
                break;
            case 5:
                _batchFadeOne = true;
                StartCoroutine(BatchFadeOutOne());
                break;
            case 6:
                _lineFive = true;
                StartCoroutine(FadeInLineFive());
                break;
            case 7:
                _lineSix = true;
                StartCoroutine(FadeInLineSix());
                break;
            case 8:
                _lineSeven = true;
                StartCoroutine(FadeInLineSeven());
                break;
            case 9:
                _lineEight = true;
                StartCoroutine(FadeInLineEight());
                break;
            case 10:
                _batchFadeTwo = true;
                StartCoroutine(BatchFadeOutTwo());
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
        }
    }

    private IEnumerator InitialLines()
    {
        SceneFade.Play("IntrofadeOne");
        for (int i = 0; i < 14; i++) //adds 13 lines to string list.
        {
            _lines.Add(_inkStory.Continue()); //adds each line of the story
            _text[i].text = _lines[i];           //to string list.
            //Debug.Log(_text[i].text);
        }
        
        yield return new WaitForSeconds(1f);
        if(_lineOne) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineOne());
    }

    #region SectionOne
    
    private IEnumerator FadeInLineOne()
    {
        lineOne_Animator.Play("LineOne_fadeIn");
        Debug.Log("waiting line one");
        yield return new WaitForSeconds(4f);
        if (_lineTwo) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineTwo());


    }
    private IEnumerator FadeInLineTwo()
    {
        lineTwo_Animator.Play("LineTwo_FadeIn");
        yield return new WaitForSeconds(2f);
        if (_lineThree) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineThree());
    }
    private IEnumerator FadeInLineThree()
    {
        lineThree_Animator.Play("LineThree_FadeIn");
        yield return new WaitForSeconds(3f);
        if(_lineFour) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineFour());
    }
    private IEnumerator FadeInLineFour()
    {
        lineFour_Animator.Play("LineFour_FadeIn");
        yield return new WaitForSeconds(5f);
        if(_batchFadeOne)yield break;
        _lineNumb++;
        StartCoroutine(BatchFadeOutOne());
    }
    private IEnumerator BatchFadeOutOne()
    {
        vignetteTwo_Animator.Play("VignetteTwo'");
        lineFour_Animator.Play("LineFour_FadeOut");
        lineThree_Animator.Play("LineThree_FadeOut");
        lineTwo_Animator.Play("LineTwo_FadeOut");
        lineOne_Animator.Play("LineOne_FadeOut");
        yield return new WaitForSeconds(1f);
        if (_lineFive) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineFive());
    }
    #endregion

    #region SectionTwo
    private IEnumerator FadeInLineFive()
    {
        lineFive_Animator.Play("LineFive_FadeIn");
        yield return new WaitForSeconds(2f);
        if (_lineSix) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineSix());
    }

    private IEnumerator FadeInLineSix()
    {
        lineSix_Animator.Play("LineSix_FadeIn");
        yield return new WaitForSeconds(2f);
        if(_lineSeven) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineSeven());
    }

    private IEnumerator FadeInLineSeven()
    {
        lineSeven_Animator.Play("LineSeven_FadeIn");
        yield return new WaitForSeconds(2f);
        if(_lineEight) yield break;
        _lineNumb++;
        StartCoroutine(FadeInLineEight());
    }
    private IEnumerator FadeInLineEight()
    {
        lineEight_Animator.Play("LineEight_FadeIn");
        yield return new WaitForSeconds(3f);
        if(_batchFadeTwo) yield break;
        _lineNumb++;
        StartCoroutine(BatchFadeOutTwo());
    }

    private IEnumerator BatchFadeOutTwo()
    {
        
        vignetteThree_Animator.Play("VignetteThree");
        lineEight_Animator.Play("LineEight_FadeOut");
        lineSeven_Animator.Play("LineSeven_FadeOut");
        lineSix_Animator.Play("LineSix_FadeOut");
        lineFive_Animator.Play("LineFive_FadeOut");
        yield return new WaitForSeconds(1f);
        if(_lineNine) yield break;
        _lineNumb++;
        StartCoroutine(LineNine());
    }

    private IEnumerator LineNine()
    {
        lineNine_Animator.Play("LineNine_FadeIn");
        yield return new WaitForSeconds(3f);
        lineNine_Animator.Play("LineNine_FadeOut");
        StartCoroutine(LineTen());

    }

    private IEnumerator LineTen()
    {
        lineTen_Animator.Play("LineTen_FadeIn");
        yield return new WaitForSeconds(3f);
        lineTen_Animator.Play("LineTen_FadeOut");
        StartCoroutine(LineEleven());
    }

    private IEnumerator LineEleven()
    {
        lineEleven_Animator.Play("LineEleven_FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine(LineTwelve());
    }

    private IEnumerator LineTwelve()
    {
        lineTwelve_Animator.Play("LineTwelve_FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine(LineThirteen());
    }

    private IEnumerator LineThirteen()
    {
        lineThirteen_Animator.Play("LineThirteen_FadeIn");
        yield return new WaitForSeconds(3f);
        lineThirteen_Animator.Play("LineThirteen_FadeOut");
        lineTwelve_Animator.Play("LineTwelve_FadeOut");
        lineEleven_Animator.Play("LineEleven_FadeOut");
        StartCoroutine(LineFourteen());
    }

    private IEnumerator LineFourteen()
    {
        lineFourteen_Animator.Play("LineFourteen_FadeIn");
        yield return new WaitForSeconds(5f);
        lineFourteen_Animator.Play("LineFourteen_FadeOut");
        cycleManager.InitialStart();
    }
    #endregion
}

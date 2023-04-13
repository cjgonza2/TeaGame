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
    
    [Header("Animators")] 
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

    [SerializeField] private List<TMP_Text> _text;

    private List<string> _lines;

    [SerializeField] private CycleManager cycleManager;

    //YOU CAN NOT USE START/UPDATE IN THE SCRIPT THAT'S PARSING THE INK FILE.
    private void Awake()
    {
        _inkStory = new Story(inkAsset.text);
        _lines = new List<string>();
        //cycleManager = CycleManager.FindInstance();
        StartCoroutine(InitialLines());
    }

    private IEnumerator InitialLines()
    {
        for (int i = 0; i < 14; i++) //adds 13 lines to string list.
        {
            _lines.Add(_inkStory.Continue()); //adds each line of the story
            _text[i].text = _lines[i];           //to string list.
            //Debug.Log(_text[i].text);
        }

        StartCoroutine(FadeInLineOne());
        yield break;
    }

    #region SectionOne
    
    private IEnumerator FadeInLineOne()
    {
        lineOne_Animator.Play("LineOne_fadeIn");
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeInLineTwo());
    }
    private IEnumerator FadeInLineTwo()
    {
        lineTwo_Animator.Play("LineTwo_FadeIn");
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInLineThree());
    }
    private IEnumerator FadeInLineThree()
    {
        lineThree_Animator.Play("LineThree_FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeInLineFour());
    }
    private IEnumerator FadeInLineFour()
    {
        lineFour_Animator.Play("LineFour_FadeIn");
        yield return new WaitForSeconds(5f);
        StartCoroutine(BatchFadeOutOne());
    }
    private IEnumerator BatchFadeOutOne()
    {
        lineFour_Animator.Play("LineFour_FadeOut");
        lineThree_Animator.Play("LineThree_FadeOut");
        lineTwo_Animator.Play("LineTwo_FadeOut");
        lineOne_Animator.Play("LineOne_FadeOut");
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeInLineFive());
    }
    #endregion

    #region SectionTwo
    private IEnumerator FadeInLineFive()
    {
        lineFive_Animator.Play("LineFive_FadeIn");
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInLineSix());
    }

    private IEnumerator FadeInLineSix()
    {
        lineSix_Animator.Play("LineSix_FadeIn");
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInLineSeven());
    }

    private IEnumerator FadeInLineSeven()
    {
        lineSeven_Animator.Play("LineSeven_FadeIn");
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeInLineEight());
    }
    private IEnumerator FadeInLineEight()
    {
        lineEight_Animator.Play("LineEight_FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine(BatchFadeOutTwo());
    }

    private IEnumerator BatchFadeOutTwo()
    {
        lineEight_Animator.Play("LineEight_FadeOut");
        lineSeven_Animator.Play("LineSeven_FadeOut");
        lineSix_Animator.Play("LineSix_FadeOut");
        lineFive_Animator.Play("LineFive_FadeOut");
        yield return new WaitForSeconds(1f);
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

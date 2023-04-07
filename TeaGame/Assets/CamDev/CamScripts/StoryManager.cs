using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

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
    [SerializeField] private Animator LineFour_Animator;
    [Header("Line Five")]
    [SerializeField] private Animator lineFive_Animator;
    [Header("Line Six")]
    [SerializeField]private Animator lineSix_Animator;
    [Header("Line Seven")]
    [SerializeField]private Animator lineSeven_Animator;
    [Header("Line Eight")]
    [SerializeField]private Animator lineEight_Animator;
    [Header("Line Nine")]
    [SerializeField]private Animator lineNine_Animator;
    [Header("Line Ten")]
    [SerializeField]private Animator lineTen_Animator;
    [Header("Line Eleven")]
    [SerializeField]private Animator lineEleven_Animator;
    [Header("Line Twelve")]
    [SerializeField]private Animator lineTwelve_Animator;
    [Header("Panel")] 
    [SerializeField] private Animator SceneFade;

    [SerializeField] private List<TMP_Text> _text;

    private List<string> _lines;

    //YOU CAN NOT USE START/UPDATE IN THE SCRIPT THAT'S PARSING THE INK FILE.
    private void Awake()
    {
        _inkStory = new Story(inkAsset.text);
        _lines = new List<string>();
        StartCoroutine(InitialLines());
    }

    private IEnumerator InitialLines()
    {
        for (int i = 0; i < 4; i++) //adds 4 lines to string list.
        {
            _lines.Add(_inkStory.Continue()); //adds each line of the story
            _text[i].text = _lines[i];           //to string list.
            //Debug.Log(_text[i].text);
        }

        StartCoroutine(FadeInLineOne());
        yield break;
    }

    #region SceneOne

    #region Line One
    private IEnumerator FadeInLineOne()
    {
        lineOne_Animator.Play("LineOne_fadeIn");
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeInLineTwo());
    }

    private IEnumerator FadeOutLineOne()
    {
        lineOne_Animator.Play("LineOne_FadeOut");
        yield return new WaitForSeconds(lineOne_Animator.GetCurrentAnimatorClipInfo(0).Length);
    }
    #endregion

    private IEnumerator FadeInLineTwo()
    {
        lineTwo_Animator.Play("LineTwo_FadeIn");
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInLineThree());
    }

    private IEnumerator FadeOutLineTwo()
    {
        lineTwo_Animator.Play("LineTwo_FadeOut");
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOutLineOne());
    }

    private IEnumerator FadeInLineThree()
    {
        lineThree_Animator.Play("LineThree_FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeInLineFour());
    }

    private IEnumerator FadeOutLineThree()
    {
        lineThree_Animator.Play("LineThree_FadeOut");
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOutLineTwo());
        yield break;
    }

    private IEnumerator FadeInLineFour()
    {
        LineFour_Animator.Play("LineFour_FadeIn");
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeInLineFive());
    }

    private IEnumerator FadeOutLineFour()
    {
        LineFour_Animator.Play("LineFour_FadeOut");
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOutLineThree());
    }

    private IEnumerator FadeInLineFive()
    {
        StartCoroutine(FadeOutLineFour());
        lineFive_Animator.Play("LineFive_FadeIn");
        yield break;
    }

    private IEnumerator FadeOutLineFive()
    {
        yield break;
    }
    #endregion

}

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
            //Debug.Log(_lines[i]);               //to string list.
        }

        foreach (TMP_Text line in _text)
        {
            for (int i = 0; i < 4; i++)
            {
                _text[i].text = _lines[i];
                Debug.Log(_text[i].text);
            }
        }

        yield break;
    }

    private IEnumerator SetLines()
    {
        yield break;
    }

    private IEnumerator FadeInLineOne()
    {
        yield break;
    }

    /*private void Start()
    {
        lineOne_Animator.SetTrigger("Active");
    }
    
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            lineOne_Animator.SetTrigger("Active");
    }*/


}

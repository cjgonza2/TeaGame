using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimationController : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private ChracterManager charManager;
    [SerializeField] private Animator characterAnimation;

    private void Start()
    {
        StartCoroutine(WaitToStartAnimation());
    }

    private IEnumerator WaitToStartAnimation()
    {
        int time = Random.Range(5, 10);
        print(time);
        print("waiting");
        yield return new WaitForSeconds(time);
        StartCoroutine($"{charManager._currentCharacter}PlayAnimation");
    }

    private IEnumerator LombardoPlayAnimation()
    {
        print("playing animation");
        characterAnimation.Play("lombardo_sleepy_anim", 0, 0f);
        yield return new WaitForSeconds(characterAnimation.GetCurrentAnimatorClipInfo(0).Length);
        if (gameManager.currentState == GameManager.State.Resting)
        {
            StartCoroutine(WaitToStartAnimation());
        }
    }

    private IEnumerator RanaPlayAnimation()
    {
        print("Playing Animation");
        characterAnimation.Play("rana_anim");
        yield return new WaitForSeconds(characterAnimation.GetCurrentAnimatorClipInfo(0).Length);
        if (gameManager.currentState == GameManager.State.Resting)
        {
            StartCoroutine(WaitToStartAnimation());
        }
    }
    
    private IEnumerator ShiWiPlayAnimation()
    {
        print("playing Animation");
        characterAnimation.Play("shiiwi_sigh_anim", 0, 0f);
        yield return new WaitForSeconds(characterAnimation.GetCurrentAnimatorClipInfo(0).Length);
        if (gameManager.currentState == GameManager.State.Resting)
        {
            StartCoroutine(WaitToStartAnimation());
        }
    }
}
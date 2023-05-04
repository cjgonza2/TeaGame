using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterAnimationController : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator characterAnimation;

    private void Start()
    {
        StartCoroutine(WaitToStartAnimation());
    }

    private IEnumerator WaitToStartAnimation()
    {
        int time = Random.Range(10, 21);
        print(time);
        print("waiting");
        yield return new WaitForSeconds(time);
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        print("playing animation");
        characterAnimation.Play("lombardo_sleepy_anim", 0, 0f);
        yield return new WaitForSeconds(characterAnimation.GetCurrentAnimatorClipInfo(0).Length);
        if (gameManager.currentState == GameManager.State.Resting)
        {
            StartCoroutine(WaitToStartAnimation());
        }
    }
}

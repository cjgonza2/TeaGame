using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterValve : MonoBehaviour
{

    [SerializeField] private Animator pourAnimation;
    [SerializeField] private Kettle_Move kettle;

    private IEnumerator PourWater()
    {
        
        pourAnimation.Play("WaterPouringAnimation");
        yield return new WaitForSeconds(pourAnimation.GetCurrentAnimatorClipInfo(0).Length);
        kettle.fill = true;
    }

    private void OnMouseDown()
    {
        Debug.Log("Pour water");
        StartCoroutine(PourWater());

    }
}

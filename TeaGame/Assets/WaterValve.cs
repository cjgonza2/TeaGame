using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaterValve : MonoBehaviour
{

    [SerializeField] private Animator pourAnimation;
    [SerializeField] private Kettle_Move kettle;
    [SerializeField] private GameManager myManager; //gamemanager reference
    [SerializeField] private AudioSource pourWaterSound;

    [Header("Pipe GameObjects")] 
    [SerializeField] private GameObject highLight;

    private IEnumerator PourWater()
    {
        pourWaterSound.Play();
        pourAnimation.Play("WaterPouringAnimation");
        yield return new WaitForSeconds(pourAnimation.GetCurrentAnimatorClipInfo(0).Length);
        kettle.fill = true;
    }

    private void OnMouseDown()
    {
        if (myManager.gamePaused) return;
        //Debug.Log("Pour water");
        StartCoroutine(PourWater());
    }

    private void OnMouseOver() //while the mouse is over;
    {
        //sets the highlight to active.
        highLight.SetActive(true);
    }

    private void OnMouseExit() //when the mouse moves away from the object;
    {
        highLight.SetActive(false); //deactivates highlight objects.
    }
}

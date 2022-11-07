using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public string trigger_name;
    public bool pouring;

    CamMove controller;

    IEnumerator AnimationDone()
    {
        yield return new WaitForSecondsRealtime(anim.GetCurrentAnimatorClipInfo(0).Length);
        anim.SetTrigger(trigger_name);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        controller = CamMove.FindInstance();
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(AnimationDone());
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.currentState == CamMove.State.Pouring)
        {
            Debug.Log("hit");
            anim.SetTrigger(trigger_name);
            //anim.SetBool("pouring", true);
        }
    }
}

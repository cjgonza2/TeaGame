using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator anim;
    public string trigger_name;

    ObjectController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = ObjectController.FindInstance();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isCollided == true)
        {
            Debug.Log("hit");
            anim.SetTrigger(trigger_name);
        }
    }
}

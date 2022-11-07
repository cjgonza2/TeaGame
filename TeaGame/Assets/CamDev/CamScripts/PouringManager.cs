using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringManager : MonoBehaviour
{

    public Animator potAnimator;
    public Animator liqAnimator;

    [SerializeField]
    private GameObject teaPot;
    [SerializeField]
    private GameObject teaLiquid;

    private bool animationDone;
    private bool canSwitch = true;
    public bool changeSprite;

    private static PouringManager instance;

    public static PouringManager FindInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
}

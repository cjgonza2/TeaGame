using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    [SerializeField] private CycleManager manager;

    private void Awake()
    {
        manager = GameObject.Find("CycleManager").GetComponent<CycleManager>();
    }

    public void ResumeGame()
    {
        manager.gameIsPaused = false;
    }
}

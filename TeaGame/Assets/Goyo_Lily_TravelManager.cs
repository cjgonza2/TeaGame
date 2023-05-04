using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Goyo_Lily_TravelManager : MonoBehaviour
{
    [SerializeField] private CycleManager cycleManager;
    [SerializeField] private TextMeshProUGUI travelLogText;

    [TextArea(3, 100)] 
    [SerializeField] private string[] travelLogs;

    private void Awake()
    {
       cycleManager = CycleManager.FindInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (cycleManager.cycleCount)
        {
            case 1:
                travelLogText.text = travelLogs[1];
                break;
            case 2:
                travelLogText.text = travelLogs[2];
                break;
            case 3:
                travelLogText.text = travelLogs[3];
                break;
        }
    }
}

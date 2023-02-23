using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoyoGardenManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] gardenPlot;

    public int gardenSpots = 3;
    public int basePriority;
    [SerializeField]
    private Vector3 plotOne;
    [SerializeField]
    private Vector3 plotTwo;
    [SerializeField] 
    private Vector3 plotThree;
    [SerializeField]
    private Vector3 plotFour;
    
    // Start is called before the first frame update
    void Start()
    {
        gardenPlot = new GameObject[gardenSpots];
        for (int i = 0; i < gardenSpots; i++)
        {
            if (i == 0)
            {
                //GameObject plot = Instantiate()
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

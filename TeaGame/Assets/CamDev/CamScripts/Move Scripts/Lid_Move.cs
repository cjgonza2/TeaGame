using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid_Move : CamMove
{
    [SerializeField] 
    private GameObject _teaParent;
    // Start is called before the first frame update
    public override void Start()
    {
       _teaParent = GameObject.Find("teapot"); 
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3()
    }
}

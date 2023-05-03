using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClumpMove : CamMove
{

    [SerializeField] private Rigidbody2D clumpRigidbody2D;

    private void Awake()
    {
        clumpRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        myManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        clumpRigidbody2D.isKinematic = _selected switch
        {
            true => true,
            false => false
        };
        print(clumpRigidbody2D.isKinematic);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaClump : MonoBehaviour
{
    [SerializeField] 
    private Pot_SpriteChanger potSpr;

    private Lid_Move _lid;

    public GameObject teaPot;
    public GameObject lid;

    public Vector2 restPos;

    private void Awake()
    {
        Debug.Log("I have arrived");
        restPos = new Vector2(0, -7.5f);
        teaPot = GameObject.Find("teapot");
        lid = GameObject.Find("teapot_lid");
        potSpr = teaPot.GetComponent<Pot_SpriteChanger>();
        _lid = lid.GetComponent<Lid_Move>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y < restPos.y)
        {
            gameObject.transform.position = restPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == ("TeaPot"))
        {
            if (potSpr._filled && _lid._colliding == false)
            {
                Debug.Log("Time to steep");
                potSpr._steeping = true;
                gameObject.transform.position = restPos;
            }
            //potSpr._steeping = true;
        }
    }
}

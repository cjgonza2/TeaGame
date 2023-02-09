using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LombardoController : MonoBehaviour
{

    private SpriteRenderer theSR;
    public Sprite idleImage;
    public Sprite reactImage;
    public Sprite sipImage;
    
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite changedImage;

    private CamMove boolCheck;
    
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        boolCheck = CamMove.FindInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (boolCheck.changeSprite == true)
        {
            theSR.sprite = changedImage;
        }
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite changedImage;
    public Sprite currentSprite;

    //[SerializeField] private PouringManager boolCheck;
    public bool fillCup;
    
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        currentSprite = defaultImage;
        //boolCheck = PouringManager.FindInstance();
    }

    // Update is called once per frame
    private void Update()
    {
        
        ChangeSprite();
        theSR.sprite = currentSprite;

        /*if (currentSprite == changedImage)
        {
            
        }
        if (boolCheck.changeSprite)
        {
            currentSprite = changedImage;
            theSR.sprite = currentSprite;
        }

        if (boolCheck.CurrentState == PouringManager.State.Resting && theSR.sprite == changedImage)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }

        if (gameObject.transform.rotation.z < -25)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -25);
        }*/
    }

    private void ChangeSprite()
    {
        if (!fillCup) return;

        currentSprite = changedImage;
    }
    
}

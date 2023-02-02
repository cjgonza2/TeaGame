using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaCompendiumController : MonoBehaviour
{
    public GameObject icon;
    public GameObject compendium;

    private SpriteRenderer iconSR;
    private Collider2D iconCollider;
    public Sprite iconDefault;
    public Sprite iconSelect;

    public bool isOver = false;


    // Start is called before the first frame update
    void Start()
    {
        
        //compendium.SetActive(false);

        iconSR = icon.GetComponent<SpriteRenderer>();
        iconCollider = icon.GetComponent<Collider2D>();
    }


    private void OnMouseOver()
    {
        isOver = true;
        iconSR.sprite = iconSelect;
    }

    private void OnMouseExit()
    {
        isOver = false;
        iconSR.sprite = iconDefault;
    }

    private void OnMouseDown()
    {
        Instantiate(compendium);
        iconSR.enabled = false;
        iconCollider.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

}

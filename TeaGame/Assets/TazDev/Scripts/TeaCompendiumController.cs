using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaCompendiumController : MonoBehaviour
{
    public GameObject icon;
    public GameObject compendium;
    public GameObject exitButton;

    private SpriteRenderer iconSR;
    private Collider2D iconCollider;
    public Sprite iconDefault;
    public Sprite iconSelect;

    public bool isOver = false;


    // Start is called before the first frame update
    void Start()
    {

        compendium.SetActive(false);

        exitButton.SetActive(false);
        Button exitCompendium = exitButton.GetComponent<Button>();
        exitCompendium.onClick.AddListener(TaskOnClick);


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
        //Instantiate(compendium);

        compendium.SetActive(true);
        iconSR.enabled = false;
        iconCollider.enabled = false;

        exitButton.SetActive(true);
        
    }

    void TaskOnClick()
    {
        Debug.Log("button press");

        exitButton.SetActive(false);
        compendium.SetActive(false);

        iconSR.enabled = true;
        iconCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

}

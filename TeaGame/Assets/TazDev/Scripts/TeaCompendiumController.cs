using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeaCompendiumController : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    
    [Header("Compendium 'Object'")] //references to compendium on work table. 
   [SerializeField] private GameObject compendium; //reference to physical compendium obj.
   [SerializeField] private SpriteRenderer compendiumSpr; //reference to physical compendium sprite renderer.

   [Header("Compendium Pages")]
    public GameObject openCompendium; //reference to open compendium obj.
    [SerializeField] private SpriteRenderer openCompendiumSpr;

    [Header("Buttons")]
    public GameObject exitButtonObj; //Exit button obj reference
    public GameObject leftButton;
    public GameObject rightButton;

    public GameObject guide;
    private Button ExitButton()
    {
        //returns the button component connected to the exit button object.
        return exitButtonObj.GetComponent<Button>();
    }

    [Header("Sprites")]
    public Sprite compendiumDefault;
    public Sprite compendiumSelect;
    
    [Header("Collider")]
    [SerializeField] private Collider2D iconCollider;

    [Header("Bools")]
    [SerializeField]private bool isOver = false;

    [SerializeField] private bool isSelected;
    public bool isOpen;

    [SerializeField] private Vector3 startPos;
    private Vector3 _restPos;

    private Vector3 _compPos()
    {
        return gameObject.transform.position;
    }

    private Vector3 _poolPos()//position that we pool the compendium to while open. 
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x,
            (gameObject.transform.position.y - 100),
            gameObject.transform.position.z);
        return pos;
    }


    // Start is called before the first frame update
    private void Start()
    {
        //sets compendium pages and page buttons to inactive. 
        openCompendium.SetActive(false);
        exitButtonObj.SetActive(false); 
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        guide.SetActive(false);

        //creates button to add listener too. 
        Button exitCompendium = ExitButton();
        exitCompendium.onClick.AddListener(TaskOnClick); //adds listener for exit button. when clicked, runs TaskOnClick function

        startPos = _compPos();
        _restPos = _poolPos();
    }

    private void Update()
    {
        if (isSelected) //if the compendium is selected
            CalculateMousePos(); //moves the compendium to where the mouse is. 
    }


    private void OnMouseOver()
    {
        if (isSelected)
        {
            compendiumSpr.sprite = compendiumDefault; //sets sprite to default when selected. 
            return; //breaks function.
        }
        //sets sprite to hover over sprite.
        isOver = true;
        compendiumSpr.sprite = compendiumSelect;
    }

    private void OnMouseExit()
    {
        isOver = false;
        compendiumSpr.sprite = compendiumDefault;
    }

    private void OnMouseDown()
    {
        isSelected = true;
        guide.SetActive(true);
    }

    private void OnMouseUp()
    {
        isSelected = false;
        guide.SetActive(false);
    }

    private void CalculateMousePos()
    {
        transform.position = _mainCam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            10));
    }
    
    private void TaskOnClick()
    {
        //sets the exit, left, and right buttons to inactive.
        //sets the compendium pages to inactive.
        isSelected = false;
        isOpen = false;
        exitButtonObj.SetActive(false);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        openCompendium.SetActive(false);
        compendium.transform.position = startPos;
        compendiumSpr.sprite = compendiumDefault; //enables sprite renderer of the small icon.
        iconCollider.enabled = true; //enables collider of small icon.
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Compendium")) return;
        isSelected = false;
        isOpen = true;
        openCompendium.SetActive(true);
        compendium.transform.position = _restPos;
        compendiumSpr.sprite = null; //disables the small icon spriterenderer.
        iconCollider.enabled = false; //disables the collider of the small icon.

        //Activates the exit, left and right button
        exitButtonObj.SetActive(true);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
    
    
}

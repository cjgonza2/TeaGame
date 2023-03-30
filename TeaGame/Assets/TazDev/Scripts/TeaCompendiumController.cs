using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeaCompendiumController : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManager manager;
    
    [Header("Buttons")]
    public GameObject icon;
    public GameObject compendium;
    public GameObject exitButton;
    public GameObject leftButton;
    public GameObject rightButton;

    [Header("Sprites")]
    public Sprite iconDefault;
    public Sprite iconSelect;
    private SpriteRenderer iconSR;
    private Collider2D iconCollider;

    [Header("Bools")]
    [SerializeField]private bool isOver = false;
    public bool isOpen;


    // Start is called before the first frame update
    private void Start()
    {

        compendium.SetActive(false); //sets the Compendium pages to not active.
        exitButton.SetActive(false);
        
        Button exitCompendium = exitButton.GetComponent<Button>();
        exitCompendium.onClick.AddListener(TaskOnClick);

        leftButton.SetActive(false);
        rightButton.SetActive(false);


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
        isOpen = true;
        compendium.SetActive(true);
        iconSR.enabled = false; //disables the small icon spriterenderer.
        iconCollider.enabled = false; //disables the collider of the small icon.

        //Activates the exit, left and right button
        exitButton.SetActive(true);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }

    private void TaskOnClick()
    {
        //sets the exit, left, and right buttons to inactive.
        //sets the compendium pages to inactive.
        isOpen = false;
        exitButton.SetActive(false);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        compendium.SetActive(false);

        iconSR.enabled = true; //enables sprite renderer of the small icon.
        iconCollider.enabled = true; //enables collider of small icon.
    }
}

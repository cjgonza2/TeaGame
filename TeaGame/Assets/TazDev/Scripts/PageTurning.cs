using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageTurning : MonoBehaviour
{
    public GameObject leftButton;
    public GameObject rightButton;

    private SpriteRenderer pageSR;
    
    public Sprite[] pages;

    public int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        pageSR = GetComponent<SpriteRenderer>();

        Button prevPage = leftButton.GetComponent<Button>();
        prevPage.onClick.AddListener(PageTurnPrev);

        
        Button nextPage = rightButton.GetComponent<Button>();
        nextPage.onClick.AddListener(PageTurnNext);
    }

    
    void PageTurnPrev()
    {
        Debug.Log("prev");
        if(currentIndex > 0)
        {
            currentIndex--;
            pageSR.sprite = pages[currentIndex];
        }
        if(currentIndex == 0)
        {
            currentIndex = 0;
        }
    }

    void PageTurnNext()
    {
        Debug.Log("next");
        if(currentIndex < pages.Length - 1)
        {
            currentIndex++;
            pageSR.sprite = pages[currentIndex];
        }

        if(currentIndex == pages.Length)
        {
            currentIndex = pages.Length;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

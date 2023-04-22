using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{

    public GameObject startButtonObj;
    //public GameObject clickToStart;


    private Button StartButton()
    {
        return startButtonObj.GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //creates button to add listener too. 
        Button openBook = StartButton();
        openBook.onClick.AddListener(TaskOnClick); //adds listener for exit button. when clicked, runs TaskOnClick function
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TaskOnClick()
    {
        Debug.Log("clicked");
        startButtonObj.SetActive(false);
        
    }
}

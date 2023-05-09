using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{
    //booleans
    private bool isOption = false;
    private bool isCredits = false;

    //button objects
    public GameObject startButtonObj;
    public GameObject playButtonObj;
    //public GameObject optionsButtonObj;
    public GameObject creditsButtonObj;
    public GameObject backButtonObj;
    public GameObject backToStart;
    //public GameObject clickToStart;

    //page objects
    public GameObject creditsPage;
    public GameObject optionsPage;

    public float startDelayTime;
    public float pageDelayTime;

    private SpriteRenderer startButtonSR;
    private Collider2D startButtonCollider;
    
    public Color startDefaultColor;
    public Color startHoverColor;

    //animations
    public Animator openBookAnim;
    public Animator zoomAnim;
    public Animator fadeOutAnim;

    //getting Button components
    private Button PlayButton()
    {
        return playButtonObj.GetComponent<Button>();
    }

    // private Button OptionsButton()
    // {
    //     return optionsButtonObj.GetComponent<Button>();
    // }

    private Button CreditsButton()
    {
        return creditsButtonObj.GetComponent<Button>();
    }

    private Button BackButton()
    {
        return backButtonObj.GetComponent<Button>();
    }

    private Button BackToStartButton()
    {
        return backToStart.GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {

        //add listener to Start Button, click for TaskOnStart
        /*Button openBook = StartButton();
        openBook.onClick.AddListener(TaskOnStart);*/

        startButtonSR = startButtonObj.GetComponent<SpriteRenderer>();
        startButtonCollider = this.GetComponent<Collider2D>();

        //add listener to Start Button, click for TaskOnStart
        Button closeBook = BackToStartButton();
        closeBook.onClick.AddListener(TaskOnClose);

        //add listener to Play Button, click for TaskOnPlay
        Button playGame = PlayButton();
        playGame.onClick.AddListener(TaskOnPlay);

        //add listener to Options Button, click for TaskOnOptions
        // Button openOptions = OptionsButton();
        // openOptions.onClick.AddListener(TaskOnOptions);

        //add listener to Credits Button, click for TaskOnCredits
        Button openCredits = CreditsButton();
        openCredits.onClick.AddListener(TaskOnCredits);

        //add listener to Back Button, click for TaskOnBack
        Button goBack = BackButton();
        goBack.onClick.AddListener(TaskOnBack);
    }


    //do this when click start button
    private void OnMouseOver()
    {
        startButtonSR.color = startHoverColor; //66340D

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("start");
            openBookAnim.Play("title book open");
            zoomAnim.Play("zoom");
            fadeOutAnim.Play("fade out");

            startButtonCollider.enabled = false;
            startButtonObj.SetActive(false);
            StartCoroutine(StartButtonDelay());
        }
        
    }

    private void OnMouseExit()
    {
        startButtonSR.color = startDefaultColor;
    }

    private void TaskOnClose()
    {
        //bring back click to start after a delay
        StartCoroutine(TitleScreenDelay());
        
        //get rid of close button
        backToStart.SetActive(false);

        //get rid of all start menu buttons
        playButtonObj.SetActive(false);
        creditsButtonObj.SetActive(false);
        // optionsButtonObj.SetActive(false);
    }

    //do this when click play button
    private void TaskOnPlay()
    {
        Debug.Log("play");
        SceneManager.LoadScene(1);
    }

    //do this when click options button
    // private void TaskOnOptions()
    // {
    //     Debug.Log("options");
    //     isOption = true;
    //
    //     //get rid of all start menu buttons
    //     backToStart.SetActive(false);
    //     playButtonObj.SetActive(false);
    //     creditsButtonObj.SetActive(false);
    //     // optionsButtonObj.SetActive(false);
    //
    //     //show options after delay
    //     StartCoroutine(PageFlipDelay());
    // }

    //do this when click credits button
    private void TaskOnCredits()
    {
        Debug.Log("credits");
        isCredits = true;

        //get rid of all start menu buttons
        backToStart.SetActive(false);
        playButtonObj.SetActive(false);
        creditsButtonObj.SetActive(false);
        // optionsButtonObj.SetActive(false);

        //show credits after delay
        StartCoroutine(PageFlipDelay());
    }

    //do this when click back button
    private void TaskOnBack()
    {
        Debug.Log("back");
        backButtonObj.SetActive(false);
        isOption = false;
        isCredits = false;

        //turn off these pages if they are open
        optionsPage.SetActive(false);
        creditsPage.SetActive(false);

        //bring back all start menu buttons
        StartCoroutine(StartButtonDelay());
    }

    IEnumerator StartButtonDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(startDelayTime);
        Debug.Log(Time.time);

        //show start menu options
        backToStart.SetActive(true);
        playButtonObj.SetActive(true);
        creditsButtonObj.SetActive(true);
        // optionsButtonObj.SetActive(true);
    }

    //when click either options or credits buttons, page flip occurs, so show objects in scene after a delay
    IEnumerator PageFlipDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(pageDelayTime);
        Debug.Log(Time.time);

        //show back button now
        backButtonObj.SetActive(true);

        if (isOption)
        {
            Debug.Log("show options");
            optionsPage.SetActive(true);
        }

        if (isCredits)
        {
            Debug.Log("show credits");
            creditsPage.SetActive(true);
        }


    }

    IEnumerator TitleScreenDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(startDelayTime);
        Debug.Log(Time.time);

        //show back button now
        startButtonObj.SetActive(true);
        startButtonCollider.enabled = true;
    }
}

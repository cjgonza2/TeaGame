using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuManager : MonoBehaviour
{

    public GameObject startButtonObj;
    public GameObject playButtonObj;
    public GameObject optionsButtonObj;
    public GameObject creditsButtonObj;
    public GameObject backButtonObj;
    //public GameObject clickToStart;

    public float delayTime;

    //getting Button components
    private Button StartButton()
    {
        return startButtonObj.GetComponent<Button>();
    }

    private Button PlayButton()
    {
        return playButtonObj.GetComponent<Button>();
    }

    private Button OptionsButton()
    {
        return optionsButtonObj.GetComponent<Button>();
    }

    private Button CreditsButton()
    {
        return creditsButtonObj.GetComponent<Button>();
    }

    private Button BackButton()
    {
        return backButtonObj.GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        //add listener to Start Button, click for TaskOnStart
        Button openBook = StartButton();
        openBook.onClick.AddListener(TaskOnStart);

        //add listener to Play Button, click for TaskOnPlay
        Button playGame = PlayButton();
        playGame.onClick.AddListener(TaskOnPlay);

        //add listener to Options Button, click for TaskOnOptions
        Button openOptions = OptionsButton();
        openOptions.onClick.AddListener(TaskOnOptions);

        //add listener to Credits Button, click for TaskOnCredits
        Button openCredits = CreditsButton();
        openCredits.onClick.AddListener(TaskOnCredits);

        //add listener to Back Button, click for TaskOnBack
        Button goBack = BackButton();
        goBack.onClick.AddListener(TaskOnBack);
    }


    //do this when click start button
    private void TaskOnStart()
    {
        Debug.Log("start");
        startButtonObj.SetActive(false);
        StartCoroutine(StartButtonDelay());
    }

    //do this when click play button
    private void TaskOnPlay()
    {
        Debug.Log("play");
    }

    //do this when click options button
    private void TaskOnOptions()
    {
        Debug.Log("options");

        //pop out back button
        backButtonObj.SetActive(true);

        //get rid of all start menu buttons
        playButtonObj.SetActive(false);
        creditsButtonObj.SetActive(false);
        optionsButtonObj.SetActive(false);
    }

    //do this when click credits button
    private void TaskOnCredits()
    {
        Debug.Log("credits");

        //pop out back button
        backButtonObj.SetActive(true);

        //get rid of all start menu buttons
        playButtonObj.SetActive(false);
        creditsButtonObj.SetActive(false);
        optionsButtonObj.SetActive(false);

    }

    //do this when click back button
    private void TaskOnBack()
    {
        Debug.Log("back");
        backButtonObj.SetActive(false);

        //bring back all start menu buttons
        playButtonObj.SetActive(true);
        creditsButtonObj.SetActive(true);
        optionsButtonObj.SetActive(true);
    }

    IEnumerator StartButtonDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(delayTime);
        Debug.Log(Time.time);

        playButtonObj.SetActive(true);
        creditsButtonObj.SetActive(true);
        optionsButtonObj.SetActive(true);
    }
}

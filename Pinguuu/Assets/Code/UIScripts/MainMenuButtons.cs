using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : ForegroundScroll
{

    //Main menussa oleva Start-nappi
    public Button mainMenuToStart;


    //Main menussa oleva Options-nappi
    //public Button mainMenuToOptions;


    //Main menussa oleva Credits-nappi
    public Button mainMenuToCredits;

    void Start()
    {
        Button start = mainMenuToStart.GetComponent<Button>();
        //Button opts = mainMenuToOptions.GetComponent<Button>();
        Button credits = mainMenuToCredits.GetComponent<Button>();
        start.onClick.AddListener(StartGame);
        //opts.onClick.AddListener(ShowOptions);
        credits.onClick.AddListener(ShowCredits);

    }

    void StartGame()
    {
        //Kutsutaan, kun main menussa painetaan startia. Lataa GameScenen.
        print("start");
        SceneManager.LoadScene("GameScene");
        Invoke("StartingLineWait", 2f);
    }

    void ShowOptions()
    {

        //Kutsutaan, kun main menussa painetaan Options. Lataa OptionsScenen
        print("Options");
        SceneManager.LoadScene("Options");
    }

    void ShowCredits()
    {
        //Kutsutaan, kun main menussa painetaan Credits. Lataa CreditsScenen.
        print("Credits");
        SceneManager.LoadScene("Credits");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : ForegroundScroll
{
    public GameObject pingviini;

    public GameObject pauseMenu;

    public GameObject gameOver;

    public Button continueButton;

    public Button mainMenuButton;

    public Button retryButton;

    public Button backToMainMenu;

    private void Start()
    {
        //Asetetaan pauseMenu-gameobjekti(canvas) inaktiiviseksi alussa.
        pauseMenu.SetActive(false);

        Button continueBtn = continueButton.GetComponent<Button>();
        Button mainMenuBtn = mainMenuButton.GetComponent<Button>();
        continueBtn.onClick.AddListener(ContinueGame);
        mainMenuBtn.onClick.AddListener(LoadMainMenu);

        //Asetetaan gameOver-gameobjekti(canvas) inaktiiviseksi alussa.
        gameOver.SetActive(false);

        Button retryBtn = retryButton.GetComponent<Button>();
        Button backToMainMenuBtn = backToMainMenu.GetComponent<Button>();
        retryBtn.onClick.AddListener(Retry);
        backToMainMenuBtn.onClick.AddListener(LoadMainMenu);
    }


    void Update()
    {
        //Pausemenu aktivoituu, kun pelaaja painaa 'P' näppäintä. Peli, distance meter ja musiikki pysähtyy. Reversoituu myös P:llä.
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        if (PlayerActions.playerHealth == 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            Camera.main.GetComponent<AudioSource>().Pause();
            isScrolling = false;
            PlayerActions.playerHealth = 3;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            //Ei tee mitään, kun käppäilee alustalla, jonka tagi on "Kenttä"
            case "Kenttä":
                break;
            //Pelaaja osuu Bottom Destroyeriin. Pysäytetään peli ja asetetaan gameOver-gameobjekti(canvas) aktiiviseksi.
            case "BottomDestroyer":
                Time.timeScale = 0f;
                gameOver.SetActive(true);
                Camera.main.GetComponent<AudioSource>().Pause();
                isScrolling = false;
                break;
        }
    }

    //Pausemenu aktivoituu, kun pelaaja painaa 'P' näppäintä. Peli, distance meter ja musiikki pysähtyy. Reversoituu myös P:llä.
    void Pause()
    {
        if (Time.timeScale == 1.0f)
        {
            //Asetetaan pauseMenu-gameobjekti(canvas) aktiiviseksi.
            pauseMenu.SetActive(true);
            //Pysäytetään musiikki.
            Camera.main.GetComponent<AudioSource>().Pause();
            //Peli säppiin
            Time.timeScale = 0f;
            isScrolling = false;
        }
        // Pausesta pois P:llä jos pause = aktiivinen & gameover = epäaktiivinen.
        else if ((Time.timeScale == 0f) && gameOver.activeSelf == false)
        {
            // jos Ptä painetaan pause menussa uudestaan asetetaan pauseMenu-canvas inaktiiviseksi ja jatketaan peliä.
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            Camera.main.GetComponent<AudioSource>().UnPause();
            isScrolling = true;
        }
    }

    //Lataa MainMenuScenen
    void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    void ContinueGame()
    {

        //pauseMenussa painetaan Continue-nappia, asetetaan pauseMenu-canvas inaktiiviseksi ja jatketaan peliä.
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isScrolling = true;
        Camera.main.GetComponent<AudioSource>().UnPause();
    }

    void Retry()
    {
        //Lataa GameScenen uudelleen.
        Invoke("StartingLineWait", 2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }
}
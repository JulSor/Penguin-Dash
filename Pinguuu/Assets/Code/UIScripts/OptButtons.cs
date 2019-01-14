using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptButtons : MonoBehaviour {

    //Options-menussa oleva Return to Main Menu-nappi.
    public Button optionsToMainMenu;
    

    void Start()
    {

        Button mainMenu = optionsToMainMenu.GetComponent<Button>();
        mainMenu.onClick.AddListener(LoadMainMenu);
        
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

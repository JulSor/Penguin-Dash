using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CredButtons : MonoBehaviour
{
    //Credits-menussa oleva Return to Main Menu-nappi.
    public Button backToMainMenu;

    void Start()
    {
        Button mainMenu = backToMainMenu.GetComponent<Button>();
        mainMenu.onClick.AddListener(LoadMainMenu);
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

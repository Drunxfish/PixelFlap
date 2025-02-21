using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{

    public Button MenuButton;


    // Start is called before the first frame update
    void Start()
    {
        // Assign event handler on the menu button object
        if (MenuButton == null) MenuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        if (MenuButton != null) MenuButton.onClick.AddListener(LoadMainMenu);

    }


    // Loads menu upon even activation with single scene view
    private void LoadMainMenu()
    {
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}

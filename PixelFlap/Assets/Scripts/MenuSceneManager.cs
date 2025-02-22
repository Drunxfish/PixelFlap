using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManagerScript : MonoBehaviour
{
    public Button PlayButton;
    public Button QuitButton;



    // Start is called before the first frame update
    void Start()
    {
        // Assign event handler on the Play/Quit button objects
        // Play
        if (PlayButton == null) PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
        if (PlayButton != null) PlayButton.onClick.AddListener(LoadGameScene);

        // Quit
        if (QuitButton == null) QuitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        if (QuitButton != null) QuitButton.onClick.AddListener(QuitGame);

    }



    // Load Game scene in single scene-view
    private void LoadGameScene()
    {
        Debug.Log("Loading Game");
        SceneManager.LoadScene("PixelFlap", LoadSceneMode.Single);
    }

    // Quit Game: close the application
    private void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}

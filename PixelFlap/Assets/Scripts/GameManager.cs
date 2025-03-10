using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Header("Game Components")]
    public Player player;
    public TextMeshProUGUI ScoreText;
    public GameObject gameOver;
    public GameObject WelcomeTitle;
    private PipeGateSpawner pipeSpawner;
    private BackgroundScript backgroundScript;

    public GameObject gameMenu;

    public int Score;
    public bool IsPlaying;

    [Header("Audio")]
    public AudioClip MenuAudio;
    private AudioSource CameraAudio;
    private bool hasWon;





    // Freeze screen when game loads
    public void Awake()
    {
        Debug.Log("Game is initializing...");
        Application.targetFrameRate = 144;
        Time.timeScale = 0f;

        // Remove/Pop UI
        player.enabled = false;
        WelcomeTitle.SetActive(true);
        gameMenu.SetActive(true);
        gameOver.SetActive(false);


        //  Finding components
        CameraAudio = Camera.main?.GetComponent<AudioSource>();
        pipeSpawner = FindObjectOfType<PipeGateSpawner>();
        backgroundScript = FindObjectOfType<BackgroundScript>();
        if (!player) player = FindObjectOfType<Player>();

        // Assurance Q.Q
        if (player) player.enabled = false;
        if (CameraAudio && MenuAudio) CameraAudio.PlayOneShot(MenuAudio, 0.7f);
    }


    // Start game 
    public void Play()
    {
        // ...
        InitializeGame();

        // Set time to normal and enable player
        Time.timeScale = 1f;
        player.enabled = true;


        // Reset Game: remove pipes etc...
        CameraAudio.Stop();
        pipeSpawner.Start();
        player.Start();
        pipeSpawner.PipesSpeed = 5;
        backgroundScript.speed = 5;

    }

    // Initialises new game and removes UI 
    public void InitializeGame()
    {
        Score = 0;
        IsPlaying = true;
        hasWon = false;
        ScoreText.text = $"SCORE: {Score}";
        gameOver.SetActive(false);
        WelcomeTitle.SetActive(false);
        gameMenu.SetActive(false);
    }

    // Pause illusion function
    public void Pause()
    {
        Debug.Log("Game Paused");
        player.enabled = false;

        if (hasWon)
        {
            return;
        }

        CameraAudio.Stop();
    }

    // End Game
    public void GameOver()
    {
        Debug.Log("Game Over");
        IsPlaying = false;

        // Bring up the Menu
        gameOver.SetActive(true);
        gameMenu.SetActive(true);
        Pause();
    }

    // Increase current score and update UI + difficulty
    public void IncreaseScore()
    {
        // for safety....
        if (!IsPlaying) return;

        // increase score/difficulty
        Score++;
        ScoreText.text = $"SCORE: {Score}";
        ChangeDifficulty();
    }


    // Change Difficulty 
    public void ChangeDifficulty()
    {
        Debug.Log("Difficulty Changed");
        pipeSpawner.PipesSpeed += 0.2f;
        backgroundScript.speed += 0.2f;
    }

}

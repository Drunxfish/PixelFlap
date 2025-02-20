using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    // Public GameOjects
    public GameObject Bird;
    public GameObject pipePrefab;
    public GameObject WingsLeft;
    public GameObject WingsRight;
    public TextMeshProUGUI ScoreText;
    public ParticleSystem collisionParticle;
    public AudioClip collisionSound;
    public bool gameOver = false;


    // General control/Private GameObjects/Audio src
    public float Gravity = 30;
    public float Jump = 10;
    public float PipeSpawnInterval = 2;
    public float PipesSpeed = 5;
    private float VerticalSpeed;
    private float PipeSpawnCountDown;
    private GameObject PipesHolder;
    private AudioSource CameraAudio;
    private AudioSource PlayerAudio;
    private int PipeCount;
    private int Score;


    // Bounds
    private int BottomBound = -6;


    // Start is called before the first frame update
    void Start()
    {
        if (!gameOver)
        {
            PlayerAudio = GetComponent<AudioSource>();
            CameraAudio = Camera.main.GetComponent<AudioSource>();


            // Reset score
            Score = 0;
            ScoreText.text = $"SCORE: {Score}";

            // Reset pipes
            PipeCount = 0;
            Destroy(PipesHolder);

            PipesHolder = new GameObject("PipesHolder");
            PipesHolder.transform.parent = this.transform;

            // Reset bird
            VerticalSpeed = 0;
            Bird.transform.position = Vector3.up * 5;

            // Reset time
            PipeSpawnCountDown = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }


        /////////// MOVEMENT ///////////
        VerticalSpeed += -Gravity * Time.deltaTime;

        // Space (key) Event handler
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Stop the bird from falling and launch it 10 vectors up
            VerticalSpeed = 0;
            VerticalSpeed += Jump;
        }

        // ###
        Bird.transform.position += Vector3.up * VerticalSpeed * Time.deltaTime;


        /////////// PIPES ///////////
        PipeSpawnCountDown -= Time.deltaTime;

        if (PipeSpawnCountDown <= 0)
        {
            PipeSpawnCountDown = PipeSpawnInterval;

            // Pipe Instantiation/Positioning
            GameObject pipe = Instantiate(pipePrefab);
            pipe.transform.parent = PipesHolder.transform;
            pipe.transform.name = (++PipeCount).ToString();


            // Pipe Pos
            pipe.transform.position += Vector3.right * 30;
            pipe.transform.position += Vector3.up * Mathf.Lerp(1, 9, UnityEngine.Random.value);
        }

        // Moving Bird(pipes) Illusion
        PipesHolder.transform.position += Vector3.left * PipesSpeed * Time.deltaTime;

        /////////// Bird Animation ///////////
        // Dive
        float speedTo01Range = Mathf.InverseLerp(-10, 10, VerticalSpeed);
        float noseAngle = Mathf.Lerp(-30, 30, speedTo01Range);
        Quaternion rotationZ = Quaternion.Euler(Vector3.forward * noseAngle);
        Quaternion rotationY = Quaternion.Euler(Vector3.up * 20);

        Bird.transform.rotation = rotationY * rotationZ;

        // // Wings
        float flapSpeed = (VerticalSpeed > 0) ? 30 : 5;
        float angle = Mathf.Sin(Time.time * flapSpeed) * 45;
        WingsLeft.transform.localRotation = Quaternion.Euler(Vector3.left * angle);
        WingsRight.transform.localRotation = Quaternion.Euler(Vector3.right * angle);


        /////////// Score ///////////
        foreach (Transform pipe in PipesHolder.transform)
        {
            // When pipes are pass the bird
            if (pipe.position.x < 0)
            {
                int pipeId = int.Parse(pipe.name);

                if (pipeId > Score)
                {
                    Score = pipeId;
                    ScoreText.text = $"SCORE: {Score}";
                }
            }

            // Destroy offscreen pipe objects
            if (pipe.position.x < -30)
            {
                Destroy(pipe.gameObject);
            }
        }

        // Check if the bird has fallen out of bounds
        if (BottomBound >= Bird.transform.position.y)
        {
            GameIsOver();
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!gameOver)
        {
            GameIsOver();
        }
    }

    public void GameIsOver()
    {
        gameOver = true;
        CameraAudio.Stop();
        collisionParticle.Play();
        PlayerAudio.PlayOneShot(collisionSound, 1f);
        Debug.Log("Game Over!");
    }
}
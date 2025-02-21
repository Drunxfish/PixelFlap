using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;


public class PipeGateSpawner : MonoBehaviour
{
    private GameObject PipesHolder;
    public GameObject pipePrefab;
    public GameManager gameManager;

    public float PipeSpawnInterval = 4;

    public float PipesSpeed = 5;

    private float PipeSpawnCountDown;

    private int PipeCount;


    // Start is called before the first frame update
    public void Start()
    {
        // get the gamemanager
        gameManager = FindObjectOfType<GameManager>();

        // Reset count for score*
        PipeCount = 0;
        Destroy(PipesHolder);

        // Pipes holder container
        PipesHolder = new GameObject("PipesHolder");
        PipesHolder.transform.parent = transform;

        // Reset time
        PipeSpawnCountDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // only spawn pipes whilst playing is active
        if (gameManager.IsPlaying)
        {
            // Each frame substract seconds
            PipeSpawnCountDown -= Time.deltaTime;

            // Respawn*
            if (PipeSpawnCountDown <= 0)
            {
                // Re-assign spawn interval
                PipeSpawnCountDown = PipeSpawnInterval;

                // Pipe Instantiation/Positioning
                GameObject pipe = Instantiate(pipePrefab);
                pipe.transform.parent = PipesHolder.transform;
                pipe.transform.name = (++PipeCount).ToString();


                // Pipe Pos
                pipe.transform.position += Vector3.right * 30;
                pipe.transform.position += Vector3.up * Mathf.Lerp(-3, 9, UnityEngine.Random.value);
            }

            // Moving Bird(pipes) Illusion
            PipesHolder.transform.position += Vector3.left * PipesSpeed * Time.deltaTime;

            // Destroy offscreen pipe objects
            foreach (Transform pipe in PipesHolder.transform)
            {
                if (pipe.position.x < -30)
                {
                    Destroy(pipe.gameObject);
                }
            }
        }

    }
}

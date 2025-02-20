using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floppy : MonoBehaviour
{
    // GameOjects
    public GameObject Bird;
    public GameObject pipePrefab;
    // General control
    public float Gravity = 30;
    public float Jump = 10;
    public float PipeSpawnInterval = 2;
    public float PipesSpeed = 5;

    private float VerticalSpeed;
    private float PipeSpawnCountDown;
    private GameObject PipesHolder;
    private int PipeCount;


    // Start is called before the first frame update
    void Start()
    {
        // Game Reset
        PipeCount = 0;
        Destroy(PipesHolder);
        
        PipesHolder = new GameObject("PipesHolder");
        PipesHolder.transform.parent = this.transform;

        // Bird reset
        VerticalSpeed = 0;
        Bird.transform.position = Vector3.up * 5;

        // Time reset
        PipeSpawnCountDown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /// MOVEMENT ///
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


        /// PIPES ///
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
            pipe.transform.position += Vector3.up * Mathf.Lerp(4, 9, UnityEngine.Random.value);
        }

        // Moving Bird(pipes) Illusion
        PipesHolder.transform.position += Vector3.left * PipesSpeed * Time.deltaTime;


    }
    private void OnTriggerEnter(Collider collider)
    {
        Start();
    }
}
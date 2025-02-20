using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Vector3 startPos;
    private float repeatWidth;
    private BirdController FlappyGame;


    private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        FlappyGame = FindObjectOfType<BirdController>();
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FlappyGame.gameOver)
        {
            if (transform.position.x < startPos.x - repeatWidth)
            {
                transform.position = startPos;
            }
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

    }

}

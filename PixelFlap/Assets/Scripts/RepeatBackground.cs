using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public Vector3 startPos;
    private float repeatWidth;
    private GameManager gameManager;

    public float speed = 5f;

    // Start is called before the first frame update
    public void Start()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Get current position
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Only move background whilst game's active
        if (gameManager.IsPlaying)
        {
            if (transform.position.x < startPos.x - repeatWidth)
            {
                transform.position = startPos;
            }
            // Update Vector
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    // Game objects
    public GameObject Bird;

    public GameObject WingsLeft;

    public GameObject WingsRight;
    public ParticleSystem collisionParticle;
    public AudioClip collisionSound;
    private AudioSource CameraAudio;
    private AudioSource PlayerAudio;
    private GameManager gameManager;

    // Control
    private float VerticalSpeed;
    public float Gravity = 30;
    public float Jump = 10;
    private float BottomBound = -6f;
    private float topBound = 14f;


    // Start is called before the first frame update
    public void Start()
    {
        // Reset bird
        VerticalSpeed = 0;
        Bird.transform.position = Vector3.up * 5;


        PlayerAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        CameraAudio = Camera.main.GetComponent<AudioSource>(); // Camera Audio SRC
    }

    // Update is called once per frame
    void Update()
    {

        /////////// MOVEMENT ///////////
        VerticalSpeed += -Gravity * Time.deltaTime;

        // Space (key), Left Mouse BTN Event handler
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Stop the bird from falling and launch it 10 vectors up
            VerticalSpeed = 0;
            VerticalSpeed += Jump;
        }

        // ###
        Bird.transform.position += Vector3.up * VerticalSpeed * Time.deltaTime;


        /////////// Bird Animation ///////////
        // Dive
        float speedTo01Range = Mathf.InverseLerp(-10, 10, VerticalSpeed);
        float noseAngle = Mathf.Lerp(-30, 30, speedTo01Range);
        Quaternion rotationZ = Quaternion.Euler(Vector3.forward * noseAngle);
        Quaternion rotationY = Quaternion.Euler(Vector3.up * 20);

        // 20 deg rotation
        Bird.transform.rotation = rotationY * rotationZ;

        // Flapping Wings
        float flapSpeed = (VerticalSpeed > 0) ? 30 : 5;
        float angle = Mathf.Sin(Time.time * flapSpeed) * 45;
        WingsLeft.transform.localRotation = Quaternion.Euler(Vector3.left * angle);
        WingsRight.transform.localRotation = Quaternion.Euler(Vector3.right * angle);

        // keep the the bird within top bound
        if (topBound <= Bird.transform.position.y)
        {
            Vector3 newYpos = Bird.transform.position;
            newYpos.y = topBound;
            Bird.transform.position = newYpos;
        }

        // Check if bird's out of bound
        if (gameManager.IsPlaying)
        {
            if (BottomBound >= Bird.transform.position.y)
            {
                setSceneCollision();
            }
        }

    }


    // Detect collisions of Obstacles/Checkpoints
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            setSceneCollision();
        }
        else if (other.gameObject.tag == "Checkpoint")
        {
            gameManager.IncreaseScore();
        }
    }

    // On collisions set Gameover thru the GameManager and set "Scene"
    private void setSceneCollision()
    {
        collisionParticle.Play();
        transform.position = transform.position;
        PlayerAudio.PlayOneShot(collisionSound, 0.7f);
        gameManager.GameOver();
        Debug.Log("Collision Sceene Active");
    }
}

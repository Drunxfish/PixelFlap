using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Get Objects/Audioclips
    public AudioSource audioSource;
    public AudioClip[] playlist;
    private int currentTrackIndex = 0;
    private BirdController FlappyGame;


    // Start is called before the first frame update
    void Start()
    {
        FlappyGame = FindObjectOfType<BirdController>();

        if (playlist.Length > 0)
        {
            PlayNextSong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!FlappyGame.gameOver)
        {
            // Play next next song
            if (!audioSource.isPlaying && playlist.Length > 0)
            {
                PlayNextSong();
            }
        }
    }
    void PlayNextSong()
    {
        // Reset Playlist 
        if (currentTrackIndex >= playlist.Length)
        {
            currentTrackIndex = 0;
        }

        // Update queue
        audioSource.clip = playlist[currentTrackIndex];
        audioSource.Play();
        currentTrackIndex++;
    }
}

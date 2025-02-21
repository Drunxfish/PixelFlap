using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Get Objects/Audioclips
    public AudioSource audioSource;
    public AudioClip[] playlist;
    private int currentTrackIndex = 0;
    private GameManager gameManager;

    // Start is called before the first frame update
    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Play next next song
        if (!audioSource.isPlaying && playlist.Length > 0 && gameManager.IsPlaying)
        {
            PlayNextSong();
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

using UnityEngine;
using System.Collections;

public class TurretAudio : MonoBehaviour {

    public AudioClip turretFiringSound;
    public AudioClip turretReloadingSound;
    public AudioClip music;

    private AudioSource audioPlayer; 

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }
	
    public void FireSound()
    {
        PlaySound(turretFiringSound);
    }

    public void ReloadSound()
    {
        PlaySound(turretReloadingSound);
    }

    void PlaySound(AudioClip clip)
    {
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }

    public bool PlayingSound ()
    {
        return audioPlayer.isPlaying;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool musicEnabled = true;
    public bool fxEnabled = true;

    [Range(0,1f)]
    public float musicVolume= 1f;
    [Range(0, 1f)]
    public float fxVolume = 1f;

    public AudioClip clearRowSound;
    public AudioClip moveSound;
    public AudioClip dropSound;
    public AudioClip errSound;
    public AudioClip[] backgroundMusic;
    public AudioClip gameOverSound;
    public AudioSource musicSource;

    public AudioClip[] vocalClip;
    public AudioClip vocalGameOver;

    public AudioClip vocalLevelUp;

    public IconToggle musicIconToggle;
    public IconToggle fxIconToggle;

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if(!musicEnabled  || !musicSource)
        {
            return;
        }

        musicSource.Stop();

        musicSource.clip = musicClip;

        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }
    private void Start()
    {
        PlayBackgroundMusic(RandomMusic());
    }
    void UpdateMusic()
    {
        if(musicSource.isPlaying != musicEnabled)
        {
            if (musicEnabled)
            {
                PlayBackgroundMusic(RandomMusic());
            }
            else
            {
                musicSource.Stop();
            }
        }
    }
    AudioClip RandomMusic()
    {
        return backgroundMusic[Random.Range(0, backgroundMusic.Length)];
    }
    public AudioClip RandomVocal()
    {
        return vocalClip[Random.Range(0, vocalClip.Length)];
    }
    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateMusic();
        musicIconToggle.ToggleIcon(musicEnabled);
    }
    public void ToggleFX()
    {
        fxEnabled = !fxEnabled;
        fxIconToggle.ToggleIcon(fxEnabled);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> musics = new List<AudioClip>();

    List<AudioClip> played = new List<AudioClip>();

    public AudioSource musicSource;
    public AudioSource sfxSource;

    bool muteState = false;

    int cheatCounter;

    public void PlayMusic()
    {
        if (musics.Count == 0)
        {
            musics = new List<AudioClip>(played);
            played.Clear();
        }

        AudioClip clip = musics[Random.Range(0, musics.Count)];
        musicSource.clip = clip;
        musicSource.Play();

        musics.Remove(clip);
        played.Add(clip);
    }

    private void Update()
    {
        if (!musicSource.isPlaying) PlayMusic();
    }

    public void SwapMute()
    {
        CheatCode();

        muteState = !muteState;
        musicSource.mute = muteState;
    }


    void CheatCode()
    {
        cheatCounter++;

        if(cheatCounter > 10)
        {
            PlayerSave.instance.currentMoney = 30000;
            PlayerSave.instance.bestLevel = 10;
            PlayerSave.instance.bestScore = 500;
            PlayerSave.instance.bestDistance = 2000;

            PlayerSave.instance.SaveData();

            cheatCounter = 0;
        }
    }
}

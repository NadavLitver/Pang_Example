using System;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static List<SoundAudioClip> clips;
    private static AudioSource TwoDimensionalAudioSource;
    public enum Sound//specify each sound here
    {
        playerHit,
        ballHit,
        levelCompleted,
        playerLost,
        playerShoot,
        UpgradeSelected,


    }
    public static void Initialize()
    {
        // this is for the list to be in the inspector but the manager static this happens once on awake
        clips = GameObject.FindObjectOfType<SoundManagerList>().Clips;
        //Creating AudioSource for static reference
        GameObject AudioSourceGO = new GameObject();
        TwoDimensionalAudioSource = AudioSourceGO.AddComponent<AudioSource>();
        AudioSourceGO.name = "TwoDimensionalAudioSource";
    }

    // Simple play once with many overloads for custom settings when playing the sound
    public static void Play(Sound sound)
    {
        TwoDimensionalAudioSource.volume = Mathf.Clamp01(GetVolumeOfClip(sound));
        if (TwoDimensionalAudioSource.volume == 0)
            return;
        TwoDimensionalAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    public static void Play(Sound sound, float volume)
    {
        TwoDimensionalAudioSource.volume = Mathf.Clamp01(volume);
        if (TwoDimensionalAudioSource.volume == 0)
            return;
        TwoDimensionalAudioSource.PlayOneShot(GetAudioClip(sound));
    }

    public static void Play(Sound sound, float volume, float pitch)
    {
        TwoDimensionalAudioSource.pitch = Mathf.Clamp(pitch, -3, 3);
        TwoDimensionalAudioSource.volume = Mathf.Clamp01(volume);
        if (TwoDimensionalAudioSource.volume == 0)
            return;
        TwoDimensionalAudioSource.PlayOneShot(GetAudioClip(sound));
    }


    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip clip in clips)
        {
            if (clip.m_Sound == sound)
            {
                return clip.m_AudioClip;
            }
        }
        return null;
    }

    public static float GetVolumeOfClip(Sound sound)
    {
        foreach (SoundAudioClip clip in clips)
        {
            if (clip.m_Sound == sound)
            {
                return clip.m_Volume;
            }
        }
        return 1;
    }
}

[Serializable]
public class SoundAudioClip
{
    public SoundAudioClip(SoundManager.Sound _sound)
    {
        m_Sound = _sound;
        name = _sound.ToString();
    }
    public string name; // The enum has a name, but Unity uses this name for the list
    public SoundManager.Sound m_Sound;
    public AudioClip m_AudioClip;
    [Range(0, 1)]
    public float m_Volume = 1;
}

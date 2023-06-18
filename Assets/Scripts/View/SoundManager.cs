using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace view
{
    public class SoundManager : ISoundManager
    {
        private AudioSource audioSource;
        private readonly SoundManagerConfig soundData;
    

        [Inject]
        public SoundManager(SoundManagerConfig _soundData)
        {
            soundData = _soundData;
            Init();
        }
        public enum Sound
        {
            playerHit,
            ballHit,
            levelCompleted,
            playerLost,
            playerShoot,
            UpgradeSelected,
        }

      

        private void Init()
        {
            
            //Creating AudioSource for Manager
            GameObject AudioSourceGO = new GameObject("SoundManager_AudioSource");
            audioSource = AudioSourceGO.AddComponent<AudioSource>();
       
        }

        // Simple play once with overloads for custom settings when playing the sound
        public void Play(Sound sound)
        {
            audioSource.volume = Mathf.Clamp01(GetVolumeOfClip(sound));
            if (audioSource.volume == 0)
                return;
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

        public void Play(Sound sound, float volume)
        {
            audioSource.volume = Mathf.Clamp01(volume);
            if (audioSource.volume == 0)
                return;
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

        public void Play(Sound sound, float volume, float pitch)
        {
            audioSource.pitch = Mathf.Clamp(pitch, -3, 3);
            audioSource.volume = Mathf.Clamp01(volume);
            if (audioSource.volume == 0)
                return;
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
        //get audio clip based on enum
        private AudioClip GetAudioClip(Sound sound)
        {
            foreach (SoundAudioClip clip in soundData.clips)
            {
                if (clip.m_Sound == sound)
                {
                    return clip.m_AudioClip;
                }
            }
            return null;
        }
        // used to get volume of clip from SO if not using a overload that specifies a volume
        private float GetVolumeOfClip(Sound sound)
        {
            foreach (SoundAudioClip clip in soundData.clips)
            {
                if (clip.m_Sound == sound)
                {
                    return clip.m_Volume;
                }
            }
            return 1;
        }
    }
}

[Serializable]
public class SoundAudioClip
{
    public SoundAudioClip(view.SoundManager.Sound _sound)
    {
        m_Sound = _sound;
        name = _sound.ToString();
    }

    public string name; // The enum has a name, but Unity uses this name for the list
    public view.SoundManager.Sound m_Sound;
    public AudioClip m_AudioClip;
    [Range(0, 1)]
    public float m_Volume = 1;
}
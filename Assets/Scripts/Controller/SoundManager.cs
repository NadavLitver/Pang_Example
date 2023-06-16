using model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace view
{
    public class SoundManager : MonoBehaviour,ISoundManager
    {
        private AudioSource twoDimensionalAudioSource;
        [SerializeField] private List<SoundAudioClip> clips;

       

        [ContextMenu("Reset List")]// to easly init list for inspector
        private void ResetList()
        {
            clips.Clear();
            foreach (SoundManager.Sound item in Enum.GetValues(typeof(SoundManager.Sound)))
            {
                clips.Add(new SoundAudioClip(item));
            }
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

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            // this is for the list to be in the inspector but the manager static this happens once on awake
            //Creating AudioSource for static reference
            GameObject AudioSourceGO = new GameObject();
            twoDimensionalAudioSource = AudioSourceGO.AddComponent<AudioSource>();
            AudioSourceGO.name = "TwoDimensionalAudioSource";
        }

        // Simple play once with many overloads for custom settings when playing the sound
        public void Play(Sound sound)
        {
            twoDimensionalAudioSource.volume = Mathf.Clamp01(GetVolumeOfClip(sound));
            if (twoDimensionalAudioSource.volume == 0)
                return;
            twoDimensionalAudioSource.PlayOneShot(GetAudioClip(sound));
        }

        public void Play(Sound sound, float volume)
        {
            twoDimensionalAudioSource.volume = Mathf.Clamp01(volume);
            if (twoDimensionalAudioSource.volume == 0)
                return;
            twoDimensionalAudioSource.PlayOneShot(GetAudioClip(sound));
        }

        public void Play(Sound sound, float volume, float pitch)
        {
            twoDimensionalAudioSource.pitch = Mathf.Clamp(pitch, -3, 3);
            twoDimensionalAudioSource.volume = Mathf.Clamp01(volume);
            if (twoDimensionalAudioSource.volume == 0)
                return;
            twoDimensionalAudioSource.PlayOneShot(GetAudioClip(sound));
        }

        private AudioClip GetAudioClip(Sound sound)
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

        private float GetVolumeOfClip(Sound sound)
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class SoundManagerList : MonoBehaviour//SoundManager list is used to connect between the enum of the sound and the audio clip and control its volume
{
    [SerializeField] List<SoundAudioClip> clips;

    public List<SoundAudioClip> Clips { get => clips;}
    [ContextMenu("Reset List")]// to easly init list for inspector
    public void ResetList()
    {
        clips.Clear();
        foreach (Sound item in Enum.GetValues(typeof(Sound)))
        {
            clips.Add(new SoundAudioClip(item));
        }
    }
}

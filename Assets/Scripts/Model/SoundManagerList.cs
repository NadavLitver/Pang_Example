using System;
using System.Collections.Generic;
using UnityEngine;
using controller;
namespace model { 
public class SoundManagerList : MonoBehaviour//SoundManager list is used to connect between the enum of the sound and the audio clip and control its volume
{
    [SerializeField] List<SoundAudioClip> clips;

    public List<SoundAudioClip> Clips { get => clips;}
    [ContextMenu("Reset List")]// to easly init list for inspector
    public void ResetList()
    {
        clips.Clear();
        foreach (SoundManager.Sound item in Enum.GetValues(typeof(SoundManager.Sound)))
        {
            clips.Add(new SoundAudioClip(item));
        }
    }
}
}

using System;
using System.Collections.Generic;
using UnityEngine;
namespace view
{
    [CreateAssetMenu(fileName = "SoundManagerConfig", menuName = "Pang/SoundManagerConfig", order = 33)]

    public class SoundManagerConfig : ScriptableObject
    {
        public List<SoundAudioClip> clips;
        [ContextMenu("Reset List")]// to easly init list for inspector
        private void ResetList()
        {
            clips.Clear();
            foreach (SoundManager.Sound item in Enum.GetValues(typeof(SoundManager.Sound)))
            {
                clips.Add(new SoundAudioClip(item));
            }
        }
    }

}
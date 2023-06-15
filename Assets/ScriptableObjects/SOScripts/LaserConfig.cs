using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LaserConfig", menuName = "Pang/Laser Config", order = 4)]

public class LaserConfig : ScriptableObject
{
    public float speed;
    public float timeToLive;//time to live
    
}

using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace model
{
    [CreateAssetMenu(fileName = "BallData", menuName = "Pang/Individual Ball Config", order = 17)]

    public class BallData : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private int splitAmount;
        [SerializeField] private float size;
        [SerializeField] BallData childData;
        public float Speed { get => speed;}
        public float Size { get => size; set => size = value; }
        public BallData ChildData { get => childData; set => childData = value; }
        public int SplitAmount { get => splitAmount;}
    }
}
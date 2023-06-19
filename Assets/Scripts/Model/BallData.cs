using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "BallData", menuName = "Pang/Individual Ball Config", order = 17)]

    public class BallData : ScriptableObject, IBallData
    {
        [SerializeField] private float speed;
        [SerializeField] private int splitAmount;
        [SerializeField] private float size;
        [SerializeField] BallData childData;
        public float Speed { get => speed;}
        public float Size { get => size; set => size = value; }
        public IBallData ChildData { get => childData; set => childData = (BallData)value; }
        public int SplitAmount { get => splitAmount;}
    }
}
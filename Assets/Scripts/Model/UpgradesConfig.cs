
using UnityEngine;
namespace model
{
    [CreateAssetMenu(fileName = "UpgradesConfig", menuName = "Pang/Upgrades Config", order = 5)]

    public class UpgradesConfig : ScriptableObject
    {
        [SerializeField] private int additionalHP;
        [SerializeField] private float newSpeed;

        public int AdditionalHP { get => additionalHP;}
        public float NewSpeed { get => newSpeed;}
    }
}
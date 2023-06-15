
using UnityEngine;
namespace model
{

    public class UpgradesData : MonoBehaviour
    {
        [SerializeField] UpgradesConfig config;
        public float newSpeed { get => config.newSpeed; }
        public int hpAddition { get => config.additionalHP; }

    }
}
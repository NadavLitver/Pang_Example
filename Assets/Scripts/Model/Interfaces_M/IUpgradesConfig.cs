using UnityEngine;

namespace model
{
    public interface IUpgradesConfig
    {
        int AdditionalHP { get; }
        float NewSpeed { get; }
    }
}
using UnityEngine.Events;

namespace controller
{
    public interface ILevelManager
    {
        int LevelCount { get; }
        UnityEvent<bool> OnEnd { get; }

        UnityEvent<int> OnAdvanceLevel { get; }
    }
}
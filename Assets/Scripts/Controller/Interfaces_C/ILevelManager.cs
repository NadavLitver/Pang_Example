using System;
namespace controller
{
    public interface ILevelManager
    {
        int LevelCount { get; }
        Action<bool> OnEnd { get; set; }

        Action<int> OnAdvanceLevel { get; set; }
    }
}
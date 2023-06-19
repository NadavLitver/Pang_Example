namespace model
{
    public interface ILevelConfig
    {
        int LevelIndex { get; }
        bool UpgradePanel { get; }
        IBallData[] BallsDatas { get; }
    }
}
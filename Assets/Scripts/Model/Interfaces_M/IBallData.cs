namespace model
{
    public interface IBallData
    {
        float Speed { get; }
        int SplitAmount { get; }
        float Size { get; set; }
        IBallData ChildData { get; set; }
    }
}
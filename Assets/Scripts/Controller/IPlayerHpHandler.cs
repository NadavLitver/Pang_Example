using UnityEngine.Events;
namespace controller
{
    public interface IPlayerHPHandler
    {
        int CurrentHealthPoints { get; }
        UnityEvent<int> HealthReducedEvent { get; }
        void PlayerHit();
        void AddHp(int hpToAdd);
        void ReduceHP(int hpToDeduct);
    }
}
using System;
namespace controller
{
    public interface IPlayerHPHandler
    {
        int CurrentHealthPoints { get; }
        Action<int> HealthReducedEvent { get; set; }
        Action<int> HealthIncreasedEvent { get; set; }

        void PlayerHit();
        void AddHp(int hpToAdd);
        void ReduceHP(int hpToDeduct);
    }
}
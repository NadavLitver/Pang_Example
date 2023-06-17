using UnityEngine;
namespace controller
{
    public interface IRobotAnimatorUpdater
    {
        void PlayShooting();
        void PlayDead();
        public Transform ShootPoint { get;}
    }
}
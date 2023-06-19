using model;
using UnityEngine;
using System;
using view;
using Zenject;

namespace controller
{
    public class PlayerHPHandler : IPlayerHPHandler// manages manipluations on playerHP
    {
        //controller
        private readonly PlayerConfig playerData;
        private readonly IBlinkOnHit blinkOnHit;
        private readonly ISoundManager soundManager;
        //data
        private float lastHit;
        private int currentHealthPoints;
        public int CurrentHealthPoints { get => currentHealthPoints; }
        //events
        public Action<int> HealthReducedEvent { get; set; }
        public Action<int> HealthIncreasedEvent { get; set; }

        [Inject]
        public PlayerHPHandler(IBlinkOnHit _blinkOnHit, ISoundManager _soundManager, PlayerConfig _PlayerConfig)
        {
            // InitReferences
            this.blinkOnHit = _blinkOnHit;
            this.soundManager = _soundManager;
            this.playerData = _PlayerConfig;

            //Init Variables
            currentHealthPoints = playerData.StartingHP;  

            //subscribe to event for blinking when getting hit
            HealthReducedEvent +=blinkOnHit.CallBlinkRoutine;
         
        }
        private bool CheckHitCooldown()//Check if getting hit is on cooldown
        {
            return Time.time - lastHit > playerData.HitCD;
        }
        public void PlayerHit()
        {
            if (CheckHitCooldown())
            {
                //reduce hp
                ReduceHP(1);
                // update last time player was hit for cooldown
                lastHit = Time.time;
                //call sound
                soundManager.Play(SoundManager.Sound.playerHit);

            }

        }
        public void AddHp(int hpToAdd)
        {
            currentHealthPoints += hpToAdd;
            HealthIncreasedEvent.Invoke(CurrentHealthPoints);


        }
        public void ReduceHP(int hpToDeduct)
        {
            currentHealthPoints -= hpToDeduct;
            HealthReducedEvent.Invoke(CurrentHealthPoints);

        }
    }
}
using model;
using UnityEngine;
using UnityEngine.Events;
using view;
using Zenject;

namespace controller
{
    public class PlayerHPHandler : IPlayerHPHandler// manages manipluations on playerHP
    {
        private readonly PlayerConfig playerData;
        private readonly IBlinkOnHit blinkOnHit;
        private readonly ISoundManager soundManager;
        private float lastHit;
        private int currentHealthPoints;
        public UnityEvent<int> HealthReducedEvent { get; private set; }

        public int CurrentHealthPoints { get => currentHealthPoints; }

        public UnityEvent<int> HealthIncreasedEvent  { get; private set; }

        [Inject]
        public PlayerHPHandler(IBlinkOnHit _blinkOnHit, ISoundManager _soundManager, PlayerConfig _PlayerConfig)
        {
            // InitReferences
            this.blinkOnHit = _blinkOnHit;
            this.soundManager = _soundManager;
            this.playerData = _PlayerConfig;

            Debug.Log("Player HP Handler Was Called");
            //Init Variables
            currentHealthPoints = playerData.StartingHP;
            HealthReducedEvent = new UnityEvent<int>();
            HealthIncreasedEvent = new UnityEvent<int>();

            //subscribe to event for blinking when getting hit
            HealthReducedEvent.AddListener(blinkOnHit.CallBlinkRoutine);
         
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
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
        private readonly IBallsPoolHandler ballsPoolHandler;
        private float lastHit;
        private int currentHealthPoints;
        public UnityEvent<int> HealthReducedEvent { get; private set; }

        public int CurrentHealthPoints { get => currentHealthPoints; }
        [Inject]
        public PlayerHPHandler(IBlinkOnHit _blinkOnHit, ISoundManager _soundManager, IBallsPoolHandler _ballsPoolHandler, PlayerConfig _PlayerConfig)
        {
            // InitReferences
            this.blinkOnHit = _blinkOnHit;
            this.soundManager = _soundManager;
            this.ballsPoolHandler = _ballsPoolHandler;
            this.playerData = _PlayerConfig;

            Debug.Log("Player HP Handler Was Called");
            //Init Variables
            currentHealthPoints = playerData.StartingHP;
            HealthReducedEvent = new UnityEvent<int>();

            //subscribe to event for blinking when getting hit
            HealthReducedEvent.AddListener(blinkOnHit.CallBlinkRoutine);
            // making it so when ball hit player, player is hit
            foreach (var ball in ballsPoolHandler.BallPoolRef.Pool)
            {
                ball.GetComponent<Ball>().OnPlayerHit.AddListener(PlayerHit);
            }
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
                currentHealthPoints--;
                // update last time player was hit for cooldown
                lastHit = Time.time;
                //invoke getting hit event
                HealthReducedEvent?.Invoke(CurrentHealthPoints);
                //call sound
                soundManager.Play(SoundManager.Sound.playerHit);

            }

        }
        public void AddHp(int hpToAdd)
        {
            currentHealthPoints += hpToAdd;
        }
        public void ReduceHP(int hpToDeduct)
        {
            currentHealthPoints -= hpToDeduct;
        }
    }
}
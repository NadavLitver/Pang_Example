using model;
using UnityEngine;
using UnityEngine.Events;
using view;

namespace controller
{
    public class PlayerHPHandler : MonoBehaviour// manages manipluations on playerHP
    {
        [SerializeField] private PlayerConfig playerData;
        private float lastHit;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BlinkOnHit blinkOnHit;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] BallsPoolHandler ballsPoolHandler;
        private int currentHealthPoints;
        public UnityEvent<int> healthReducedEvent;

        public int CurrentHealthPoints { get => currentHealthPoints; }

        private void Start()
        {
            currentHealthPoints = playerData.StartingHP;

            //check if player lost
            healthReducedEvent.AddListener(gameManager.CheckLose);

            healthReducedEvent.AddListener(blinkOnHit.CallBlinkRoutine);
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
                //reduce score
                gameManager.ReduceScore(50);
                // update last time player was hit for cooldown
                lastHit = Time.time;
                //invoke getting hit event
                healthReducedEvent?.Invoke(CurrentHealthPoints);
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
using model;
using UnityEngine;
using UnityEngine.Events;
using view;
using Zenject;

namespace controller
{
    public class PlayerHPHandler : MonoBehaviour, IPlayerHPHandler// manages manipluations on playerHP
    {
        [SerializeField] private PlayerConfig playerData;
        private float lastHit;
        [Inject] private IGameManager gameManager;
        [Inject] private IBlinkOnHit blinkOnHit;
        [Inject] private ISoundManager soundManager;
        [Inject] private IBallsPoolHandler ballsPoolHandler;
        private int currentHealthPoints;
        public UnityEvent<int> HealthReducedEvent { get; private set; }

        public int CurrentHealthPoints { get => currentHealthPoints; }
        private void Awake()
        {
            currentHealthPoints = playerData.StartingHP;
            HealthReducedEvent = new UnityEvent<int>();

        }
        private void Start()
        {
            //check if player lost on health reduced
            HealthReducedEvent.AddListener(gameManager.CheckLose);

            HealthReducedEvent.AddListener(blinkOnHit.CallBlinkRoutine);
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
using model;
using UnityEngine;
using UnityEngine.Events;

namespace controller
{
    public class PlayerHPHandler : MonoBehaviour// manages manipluations on playerHP
    {
        [SerializeField] PlayerData playerData;
        private float lastHit;
        [SerializeField] GameManager gameManager;
        private int currentHealthPoints;
        public UnityEvent<int> healthReducedEvent;

        public int CurrentHealthPoints { get => currentHealthPoints; }

        private void Start()
        {
            currentHealthPoints = playerData.StartHP;

            //check if player lost
            healthReducedEvent.AddListener(gameManager.CheckLose);
         
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
                SoundManager.Play(SoundManager.Sound.playerHit);

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
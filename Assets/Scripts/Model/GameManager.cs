using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using view;
namespace model
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour // GameManager: Manages the game state, score, health
    {
        [SerializeField] int startingHealthPoints;
        [SerializeField] float playerHitCD = 1;
        [SerializeField] LevelManager levelManager;
        [SerializeField] UIHandler UIHandlerRef;
        [SerializeField] RobotAnimatorUpdater robotAnimatorUpdater;

        private int CurrentHealthPoints;
        private float lastHit;
        private float score;

        public UnityEvent<int> healthReducedEvent;
        public UnityEvent OnLose;

        public float Score { get => score; }
        private void Awake()
        {
            //make sure timescale is one
            Time.timeScale = 1;
            // init variables
            CurrentHealthPoints = startingHealthPoints;
            score = 0;
            //Init the static soundmanager
            SoundManager.Initialize();
        }
        private void Start()
        {

            // update UI on beggining of game
            UIHandlerRef.UpdateLevel(levelManager.levelCount);
            UIHandlerRef.UpdateScore((int)score);
            UIHandlerRef.UpdateHealth(CurrentHealthPoints);

            //Subscribe to events

            //call on update score when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UpdateScoreOnLevelAdvance);
            //call on update health when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UpdateHealthOnLevelAdvance);
            //update level in ui when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UIHandlerRef.UpdateLevel);
            //Call the 3 2 1 countdown on screen
            levelManager.OnAdvanceLevel.AddListener(UIHandlerRef.CallCountdownRoutine);
            //check if player lost
            healthReducedEvent.AddListener(CheckLose);
            //update health in ui when hit
            healthReducedEvent.AddListener(UIHandlerRef.UpdateHealth);
            // On Finished All levels Call UI Handler
            levelManager.OnWin.AddListener(UIHandlerRef.EnableEndingPanel);
            // subscribe robot animator death anim to game lost
            OnLose.AddListener(robotAnimatorUpdater.PlayDead);
        }
        public void PlayerHit()
        {
            if (CheckHitCooldown())
            {
                //reduce hp
                CurrentHealthPoints--;
                //reduce score
                score -= 50;
                // update last time player was hit for cooldown
                lastHit = Time.time;
                //invoke getting hit event
                healthReducedEvent?.Invoke(CurrentHealthPoints);
                //call sound
                SoundManager.Play(SoundManager.Sound.playerHit);

            }

        }
        public void CheckLose(int currentHealthPoints)
        {
            if (currentHealthPoints < 0)
            {
                OnLose?.Invoke();
                Time.timeScale = 0;
                UIHandlerRef.EnableEndingPanel(false);
                
                SoundManager.Play(SoundManager.Sound.playerLost);
            }
        }
        internal void UpdateScoreOnSplitBall(LaserHandler laser, Rigidbody2D ball)
        {
            score += 10 * ball.transform.localScale.x;
            //call on update ui
            UIHandlerRef.UpdateScore((int)score);
        }
        void UpdateScoreOnLevelAdvance(int level)
        {
            score += 100 * level;

            //call on update ui
            UIHandlerRef.UpdateScore((int)score);
            UIHandlerRef.UpdateLevel(level);

        }
        void UpdateHealthOnLevelAdvance(int level)
        {
            //increase health points by 1
            CurrentHealthPoints++;
            // update in UI
            UIHandlerRef.UpdateHealth(CurrentHealthPoints);

        }
        internal void AddHealthPoints(int healthPoints)
        {
            CurrentHealthPoints += healthPoints;
            // update in UI
            UIHandlerRef.UpdateHealth(CurrentHealthPoints);
        }
        private bool CheckHitCooldown()//Check if getting hit is on cooldown
        {
            return Time.time - lastHit > playerHitCD;
        }
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using view;

namespace controller
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour // GameManager: Manages the game state, score
    {
        [SerializeField] LevelManager levelManager;
        [SerializeField] UIHandler uIHandler;
        [SerializeField] PlayerHPHandler playerHitHandler;
        [SerializeField] SoundManager soundManager;
        private float score;//no need for so if score is going to constantly change and it inits at 0


        public UnityEvent OnLose;
        public UnityEvent<bool> OnEnd;

        public float Score { get => score; }
        private void Awake()
        {
            //make sure timescale is one
            Time.timeScale = 1;
            // init variables
            score = 0;
            //Init the static soundmanager
           
        }
        private void Start()
        {

            // update UI on beggining of game
            uIHandler.UpdateLevel(levelManager.levelCount);
            uIHandler.UpdateScore((int)score);
            uIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);

            //Subscribe to events

            //call on update score when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UpdateScoreOnLevelAdvance);
            //call on update health when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UpdateHealthOnLevelAdvance);
            //update level in ui when advancing a level
            levelManager.OnAdvanceLevel.AddListener(uIHandler.UpdateLevel);
            //Call the 3 2 1 countdown on screen
            levelManager.OnAdvanceLevel.AddListener(uIHandler.CallCountdownRoutine);
            //update health in ui when hit
            playerHitHandler.healthReducedEvent.AddListener(uIHandler.UpdateHealth);
            // On Finished All levels Call UI Handler
            OnEnd.AddListener(uIHandler.EnableEndingPanel);
        }

        internal void CheckLose(int currentHealthPoints)
        {
            if (currentHealthPoints < 0)
            {
                OnLose?.Invoke();
                Time.timeScale = 0;
                uIHandler.EnableEndingPanel(false);

                soundManager.Play(SoundManager.Sound.playerLost);
            }
        }
        internal void UpdateScoreOnSplitBall(LaserHandler laser, Rigidbody2D ball)
        {
            score += 10 * ball.transform.localScale.x;
            //call on update ui
            uIHandler.UpdateScore((int)score);
        }
        private void UpdateScoreOnLevelAdvance(int level)
        {
            score += 100 * level;

            //call on update ui
            uIHandler.UpdateScore((int)score);
            uIHandler.UpdateLevel(level);

        }
        private void UpdateHealthOnLevelAdvance(int level)
        {
            //increase health points by 1
            playerHitHandler.AddHp(1);
            // update in UI
            uIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);

        }
        internal void AddHealthPointsAndUpdateUI(int healthPoints)
        {
            playerHitHandler.AddHp(healthPoints);
            // update in UI
            uIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);
        }
        internal void ReduceScore(float scoreToDeduct)
        {
            score -= scoreToDeduct;
        }
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using view;
using Zenject;

namespace controller
{
    [DefaultExecutionOrder(-1)]
    public class GameManager : MonoBehaviour , IGameManager// GameManager: Manages the game state, score
    {
        [Inject] ILevelManager levelManager;
        [Inject] IUIHandler iUIHandler;
        [Inject] IPlayerHPHandler playerHitHandler;
        [Inject] private ISoundManager soundManager;

        private float score;
        public UnityEvent OnLose { get; private set; }
        public UnityEvent<bool> OnEnd { get; private set; }
        public float Score { get => score; }
        private void Awake()
        {
            OnEnd = new UnityEvent<bool>();
            OnLose = new UnityEvent();
            //make sure timescale is one
            Time.timeScale = 1;
            // init variables
            score = 0;
           
        }
        private void Start()
        {

            // update UI on beggining of game
            iUIHandler.UpdateLevel(levelManager.LevelCount);
            iUIHandler.UpdateScore((int)score);
            iUIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);

            //Subscribe to events ->

            //call on update score when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UpdateScoreOnLevelAdvance);
            //call on update health when advancing a level
            levelManager.OnAdvanceLevel.AddListener(UpdateHealthOnLevelAdvance);
            //update level in ui when advancing a level
            levelManager.OnAdvanceLevel.AddListener(iUIHandler.UpdateLevel);
            //Call the 3 2 1 countdown on screen
            levelManager.OnAdvanceLevel.AddListener(iUIHandler.CallCountdownRoutine);
            //update health in ui when hit
            playerHitHandler.HealthReducedEvent.AddListener(iUIHandler.UpdateHealth);
            // On Finished All levels Call UI Handler
            OnEnd.AddListener(iUIHandler.EnableEndingPanel);
        }

        public void CheckLose(int currentHealthPoints)
        {
            if (currentHealthPoints < 0)
            {
                OnLose?.Invoke();
                Time.timeScale = 0;
                iUIHandler.EnableEndingPanel(false);

                soundManager.Play(SoundManager.Sound.playerLost);
            }
        }
        public void UpdateScoreOnSplitBall(ILaserHandler laser, Rigidbody2D ball)
        {
            score += 10 * ball.transform.localScale.x;
            //call on update ui
            iUIHandler.UpdateScore((int)score);
        }
        private void UpdateScoreOnLevelAdvance(int level)
        {
            score += 100 * level;

            //call on update ui
            iUIHandler.UpdateScore((int)score);
            iUIHandler.UpdateLevel(level);

        }
        private void UpdateHealthOnLevelAdvance(int level)
        {
            //increase health points by 1
            playerHitHandler.AddHp(1);
            // update in UI
            iUIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);

        }
        public void AddHealthPointsAndUpdateUI(int healthPoints)
        {
            playerHitHandler.AddHp(healthPoints);
            // update in UI
            iUIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);
        }
        public void ReduceScore(float scoreToDeduct)
        {
            score -= scoreToDeduct;
        }
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
    }
}
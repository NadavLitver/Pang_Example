
using model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using view;
using Zenject;

namespace controller
{

    public class GameManager : IGameManager// GameManager: Manages the game state, score
    {
        private readonly ILevelManager levelManager;
        private readonly IUIHandler iUIHandler;
        private readonly IPlayerHPHandler playerHitHandler;
        private readonly ISoundManager soundManager;


        private float score;
        public UnityEvent OnLose { get; private set; }
        public float Score { get => score; }

        

        [Inject]
        public GameManager(ILevelManager _levelManager, IUIHandler _IUIHandler, IPlayerHPHandler _playerHitHandler, ISoundManager _soundManager)
        {


            this.levelManager = _levelManager;
            this.iUIHandler = _IUIHandler;
            this.playerHitHandler = _playerHitHandler;
            this.soundManager = _soundManager;

            OnLose = new UnityEvent();
            //make sure timescale is one
            Time.timeScale = 1;
            // init variables
            score = 0;
            //set up UI
            InitUI();
            //set up Events
            InitEvents();
      
        }

        private void InitUI()
        {
            // update UI on beggining of game
            iUIHandler.UpdateLevel(levelManager.LevelCount);
            iUIHandler.UpdateScore((int)score);
            iUIHandler.UpdateHealth(playerHitHandler.CurrentHealthPoints);
        }
        private void InitEvents()
        {



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
            //deduct score when hit
            playerHitHandler.HealthReducedEvent.AddListener(ReduceScoreOnHit);
            //chech if lost when hit
            playerHitHandler.HealthReducedEvent.AddListener(CheckLose);
            //update health in ui when gained HP
            playerHitHandler.HealthIncreasedEvent.AddListener(iUIHandler.UpdateHealth);
            // On Finished All levels Call UI Handler
            levelManager.OnEnd.AddListener(iUIHandler.EnableEndingPanel);

            

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
        public void UpdateScoreOnSplitBall(ILaserHandler laser, Rigidbody2D ballRB)
        {
            score += 50 * ballRB.transform.localScale.x;
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
      

        }
        public void ReduceScoreOnHit(int remainingHealth)
        {
            int scoreToDeduct = 100 / remainingHealth;
            score -= scoreToDeduct;
        }
        
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
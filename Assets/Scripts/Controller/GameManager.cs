using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using view;
using Zenject;

namespace controller
{

    public class GameManager : IGameManager// GameManager: Manages the game state, score
    {
        //controllers
        private readonly ILevelManager levelManager;
        private readonly IUIHandler iUIHandler;
        private readonly IPlayerHPHandler playerHitHandler;
        private readonly ISoundManager soundManager;
        //data
        private float score;
        public float Score { get => score; }
        //events
        public UnityEvent OnLose { get; private set; }



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

        public void CheckLose(int currentHealthPoints)//check if player lost, if he did call "OnLost"
        {
            if (currentHealthPoints < 0)
            {
                OnLost();
            }
        }

        private void OnLost()//method executed when player loses
        {
            OnLose?.Invoke();//invoke event
            Time.timeScale = 0;//stop the game using timescale
            iUIHandler.EnableEndingPanel(false);// open a losing ui panel
            soundManager.Play(SoundManager.Sound.playerLost);// play a losing sound
        }

        public void UpdateScoreOnSplitBall(ILaserHandler laser, Rigidbody2D ballRB)
        {
            //increase score based on balls size (the smaller the bigger the score gain is)
            score += 50 / ballRB.transform.localScale.x;
            //call on update ui
            iUIHandler.UpdateScore((int)score);
        }
        private void UpdateScoreOnLevelAdvance(int level)
        {
            score += 100 * level;//increase score based on level

            //call on update ui
            iUIHandler.UpdateScore((int)score);
            iUIHandler.UpdateLevel(level);

        }
        private void UpdateHealthOnLevelAdvance(int level)//increase health points by 1
        {
            playerHitHandler.AddHp(1);
        }
        public void ReduceScoreOnHit(int remainingHealth)
        {
            if (remainingHealth <= 0)
                return;//make sure we dont divide by zero

            int scoreToDeduct = 100 / remainingHealth;
            score -= scoreToDeduct;//reduce score based on remaining health of player (more health = less score lost)
        }

    
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using view;
using Zenject;

namespace controller
{
    public class LevelManager : MonoBehaviour, ILevelManager// LevelGenerator: Generates and manages the arrangement of game objects within each level.
    {
        //data
        [SerializeField] private model.LevelConfig[] levelConfigs;
        //controllers
        [Inject] private IBallController ballsController;
        [Inject] private IUpgradeHandler upgradeHandler;
        [Inject] private ISoundManager soundManager;


        private float delayBetweenLevels;
        public int LevelCount { get; private set; }
        public UnityEvent<int> OnAdvanceLevel { get; private set; }
        public UnityEvent<bool> OnEnd { get; private set; }

        private void Awake()
        {
            LevelCount = 1;
            delayBetweenLevels = 4.5f;
            OnAdvanceLevel = new UnityEvent<int>();
            OnEnd = new UnityEvent<bool>();
        }
        private void Start()
        {
          
            StartCoroutine(LevelsRoutine());
            
        }
        private IEnumerator LevelsRoutine()
        {
            foreach (var level in levelConfigs)
            {
                //update level index
                LevelCount = level.levelIndex;
                OnAdvanceLevel?.Invoke(LevelCount);
                //check to open upgrade panel
                if (level.upgradePanel)
                {//Open Upgrade panel, yielding so it waits for the other routine to finish
                    yield return upgradeHandler.UpgradeRoutine();
                }
                yield return new WaitForSeconds(delayBetweenLevels);// small delay between levels, scaling with timescale

                int ballCount = level.ballCount;
                float ballSize = level.ballSize;

                for (int i = 0; i < ballCount; i++)
                {
                    // Spawn a ball using the ball controller and set the position,scale,direction
                    ballsController.CreateBall(new Vector2(Random.Range(0,5), Random.Range(0, 2)), Vector2.one * ballSize, ballsController.RandomBallVelocity());
                }

                //wait till all balls are destroyed to start new level
                yield return new WaitUntil(() => ballsController.IsActiveBallsEmpty());
                //call level completed sound
                soundManager.Play(SoundManager.Sound.levelCompleted);





            }
            // after all levels are done the player has won and an event is raised
            OnEnd?.Invoke(true);

        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    [DefaultExecutionOrder(1)]// delay script execution
    public class LevelManager : MonoBehaviour// LevelGenerator: Generates and manages the arrangement of game objects within each level.
    {
        //data
        [SerializeField] private model.LevelConfig[] levelConfigs;
        //controllers
        [SerializeField] BallController ballsController;
        [SerializeField] UpgradeHandler upgradeHandler;
        [SerializeField] GameManager gameManager;

        private float delayBetweenLevels;
        internal int levelCount;
        public UnityEvent<int> OnAdvanceLevel;
        private void Awake()
        {
            levelCount = 1;
            delayBetweenLevels = 4.5f;
        }
        private void Start()
        {
            StartCoroutine(LevelsRoutine());
            
        }
        IEnumerator LevelsRoutine()
        {
            foreach (var level in levelConfigs)
            {
                //update level index
                levelCount = level.levelIndex;
                OnAdvanceLevel?.Invoke(levelCount);
                //check to open upgrade panel
                if(level.upgradePanel)
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
                SoundManager.Play(SoundManager.Sound.levelCompleted);





            }
            // after all levels are done the player has won and an event is raised
            gameManager.OnEnd?.Invoke(true);

        }
    }
}
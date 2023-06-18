using Cysharp.Threading.Tasks;
using model;
using System;
using UnityEngine;
using UnityEngine.Events;
using view;
using Zenject;

namespace controller
{
    public class LevelManager : ILevelManager// LevelGenerator: Generates and manages the arrangement of game objects within each level.
    {
        //data
        [Inject] LevelConfigList levels;
        //controllers
        [Inject] private IBallController ballsController;
        [Inject] private IUpgradeHandler upgradeHandler;
        [Inject] private ISoundManager soundManager;

        //data
        private float delayBetweenLevels;
        public int LevelCount { get; private set; }
        //events
        public UnityEvent<int> OnAdvanceLevel { get; } = new UnityEvent<int>();
        public UnityEvent<bool> OnEnd { get; } = new UnityEvent<bool>();
        [Inject]
        public LevelManager(IBallController _ballsController, IUpgradeHandler _upgradeHandler, ISoundManager _soundManager, LevelConfigList _levels)
        {
            ballsController = _ballsController;
            upgradeHandler = _upgradeHandler;
            soundManager = _soundManager;
            levels = _levels;

            //init variables
            LevelCount = 1;
            delayBetweenLevels = 4.5f;
            //init events
            _ = LevelsRoutine();
        }

        private async UniTask LevelsRoutine()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(.15));
            foreach (LevelConfig level in levels.LevelConfigs)
            {
                // update level index
                LevelCount = level.LevelIndex;
                OnAdvanceLevel?.Invoke(LevelCount);

                // check to open upgrade panel
                if (level.UpgradePanel)
                {
                    await upgradeHandler.UpgradeRoutine();
                }

                await UniTask.Delay(TimeSpan.FromSeconds(delayBetweenLevels)); // small delay between levels, scaling with timescale

                int ballCount = level.BallsDatas.Length;

                for (int i = 0; i < ballCount; i++)
                {
                    float ballSize = level.BallsDatas[i].Size;
                    float ballSpeed = level.BallsDatas[i].Speed;

                    // Spawn a ball using the ball controller and set the position, scale, direction
                    IBall currentBall = ballsController.CreateBall(new Vector2(UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 2)), Vector2.one * ballSize, ballsController.RandomBallVelocity(ballSpeed));
                    currentBall.ballData = level.BallsDatas[i];
                }

                // wait until all balls are destroyed to start a new level
                await UniTask.WaitUntil(() => ballsController.IsActiveBallsEmpty());

                // call level completed sound
                soundManager.Play(SoundManager.Sound.levelCompleted);
            }

            // after all levels are done, the player has won and an event is raised
            OnEnd?.Invoke(true);
        }
    }
}
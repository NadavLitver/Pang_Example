using UnityEngine;
using Zenject;

namespace controller
{
    public class RobotAnimatorUpdater : IRobotAnimatorUpdater,ITickable// animator updater updates the robot animator and sprite renderer based on the controller input handler
    {
        //controller
        [Inject] private readonly IInputHandler inputHandler;
        [Inject] private readonly IGameManager gameManager;

        //Refrences to components
        [Inject] private readonly Animator animator;
        [Inject] private readonly SpriteRenderer robotSR;
        [Inject] private readonly Transform shootPoint;
        public Transform ShootPoint => shootPoint;

        //hashes
        private int runningHash;
        private int shootingHash;
        private int deadHash;
        public RobotAnimatorUpdater(IInputHandler inputHandler, IGameManager gameManager, Animator animator, SpriteRenderer robotSR, Transform shootPoint)
        {
            this.inputHandler = inputHandler;
            this.gameManager = gameManager;
            this.animator = animator;
            this.robotSR = robotSR;
            this.shootPoint = shootPoint;

            // subscribe to on lose
            gameManager.OnLose += PlayDead;
            //cache to hashes
            runningHash = Animator.StringToHash("Running");
            shootingHash = Animator.StringToHash("Shooting");
            deadHash = Animator.StringToHash("Dead");
        }

    
      
        public void PlayShooting()
        {
            animator.Play(shootingHash);
        }
        public void PlayDead()
        {
            animator.Play(deadHash);
        }

        public void Tick()
        {
            int horInput = inputHandler.GetHorInput();//get input from input handler
            //set running based on input
            animator.SetBool(runningHash, horInput != 0);
            if (horInput != 0)
            {//flip model
                robotSR.flipX = horInput == -1;
            }
        }
    }
}
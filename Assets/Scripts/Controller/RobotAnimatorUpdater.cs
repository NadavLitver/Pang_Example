using UnityEngine;
using Zenject;

namespace controller
{
    public class RobotAnimatorUpdater : MonoBehaviour , IRobotAnimatorUpdater// animator updater updates the robot animator and sprite renderer based on the controller input handler
    {
        [Inject] private readonly IInputHandler inputHandler;
        [Inject] private readonly IGameManager gameManager;
        [Inject] private readonly Animator animator;
        [Inject] private readonly SpriteRenderer robotSR;
        [Inject] private readonly Transform shootPoint;

        //hashes
        private int runningHash;
        private int shootingHash;
        private int deadHash;

        public Transform ShootPoint => shootPoint;

        private void Start()
        {
            // subscribe to on lose
            gameManager.OnLose.AddListener(PlayDead);
            //cache to hashes
            runningHash = Animator.StringToHash("Running");
            shootingHash = Animator.StringToHash("Shooting");
            deadHash = Animator.StringToHash("Dead");
          
        }
        private void Update()
        {
            int horInput = inputHandler.GetHorInput();
            //set running based on input
            animator.SetBool(runningHash, horInput != 0);
            if (horInput != 0)
            {//flip model
                robotSR.flipX = horInput == -1;
            }
        }
        public void PlayShooting()
        {
            animator.Play(shootingHash);
        }
        public void PlayDead()
        {
            animator.Play(deadHash);
        }
    }
}
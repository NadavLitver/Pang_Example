using controller;
using UnityEngine;

namespace view
{
    [RequireComponent(typeof(Animator))]
    public class RobotAnimatorUpdater : MonoBehaviour// animator updater updates the robot animator and sprite renderer based on the controller input handler
    {
        [SerializeField] InputHandler inputHandler;
        [SerializeField] Animator animator;
        [SerializeField] SpriteRenderer robotSR;
        [SerializeField] GameManager gameManager;
        private int RunningHash;
        private int ShootingHash;
        private int DeadHash;

        private void Start()
        {
            //cache names to hashes
            gameManager.OnLose.AddListener(PlayDead);
            RunningHash = Animator.StringToHash("Running");
            ShootingHash = Animator.StringToHash("Shooting");
            DeadHash = Animator.StringToHash("Dead");

            if (animator == null) { animator = GetComponent<Animator>(); }
        }
        private void Update()
        {
            int horInput = inputHandler.GetHorInput();
            //set running based on input
            animator.SetBool(RunningHash, horInput != 0);
            if (horInput != 0)
            {//flip model
                robotSR.flipX = horInput == -1;
            }
        }
        public void PlayShooting()
        {
            animator.Play(ShootingHash);
        }
        public void PlayDead()
        {
            animator.Play(DeadHash);
        }
    }
}
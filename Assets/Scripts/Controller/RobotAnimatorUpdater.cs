using UnityEngine;

namespace controller
{
    public class RobotAnimatorUpdater : MonoBehaviour// animator updater updates the robot animator and sprite renderer based on the controller input handler
    {
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer robotSR;
        [SerializeField] private GameManager gameManager;

        //hashes
        private int runningHash;
        private int shootingHash;
        private int deadHash;

        private void Start()
        {
            //cache names to hashes
            gameManager.OnLose.AddListener(PlayDead);
            runningHash = Animator.StringToHash("Running");
            shootingHash = Animator.StringToHash("Shooting");
            deadHash = Animator.StringToHash("Dead");
            if (animator == null) { animator = GetComponent<Animator>(); }
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
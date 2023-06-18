using UnityEngine;
using UnityEngine.UI;
using view;
using Zenject;

namespace controller
{
    public class MenuPanel : IMenuPanel//the menu panel is responsible for handling the pause menu logic which is also the main menu
    {
        //data
        private readonly GameObject pauseMenuUI;
        private readonly Button pauseButton;
        private readonly Button startButton;
        private readonly Button resetButton;
        private bool isPaused;
        private bool didResumeOnce;
        //controllers
        private readonly IUIHandler iUIhandler;
        private readonly IInputHandler inputHandler;


        [Inject]
        private MenuPanel(IUIHandler _iUIhandler,
                          IInputHandler _inputHandler,
                          GameObject _pauseMenuUI,
                          [Inject(Id = "PauseButton")] Button _pauseButton,
                          [Inject(Id = "StartButton")] Button _startButton,
                          [Inject(Id = "ResetButton")] Button _resetButton)

        {
            iUIhandler = _iUIhandler;
            inputHandler = _inputHandler;
            pauseMenuUI = _pauseMenuUI;
            pauseButton = _pauseButton;
            startButton = _startButton;
            resetButton = _resetButton;

            pauseButton.onClick.AddListener(TogglePauseMenu);
            startButton.onClick.AddListener(TogglePauseMenu);
            inputHandler.OnTapScreen.AddListener(CheckToUnpause);
            TogglePauseMenu();
        }

        private void CheckToUnpause()//if player pressed somewhere that is not ui and game is paused, unpause
        {
            if (isPaused)
            {
                TogglePauseMenu();
            }
        }



        public void TogglePauseMenu()
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f; // Pause the game by setting time scale to 0
                pauseMenuUI.SetActive(true);


            }
            else
            {
                Time.timeScale = 1f; // Resume the game by setting time scale back to 1
                pauseMenuUI.SetActive(false);
                if (!didResumeOnce)//make sure setting text once
                {
                    iUIhandler.UpdateStartText();
                    didResumeOnce = true;
                    resetButton.gameObject.SetActive(true);
                }


            }
        }

    }
}

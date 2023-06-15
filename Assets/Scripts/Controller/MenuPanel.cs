using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using view;

namespace controller
{
    public class MenuPanel : MonoBehaviour//the menu panel is responsible for handling the pause menu logic which is also the main menu
    {
        public GameObject pauseMenuUI;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Button resetButton;

        [SerializeField] private UIHandler uIhandler;

        private bool isPaused;
        private bool didResumeOnce;
        private void Start()
        {
            pauseButton.onClick.AddListener(TogglePauseMenu);
            startButton.onClick.AddListener(TogglePauseMenu);
            TogglePauseMenu();
        }
        private void Update()
        {
            // Check for a pause button press on mobile
            if (((Input.GetMouseButtonDown(0) || (Input.touchCount > 0)) && !IsPointerOverUIObject() && isPaused))
            {
                TogglePauseMenu();
            }
        }

        private void TogglePauseMenu()
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
                    uIhandler.UpdateStartText();
                    didResumeOnce = true;
                    resetButton.gameObject.SetActive(true);
                }
               

            }
        }
        private bool IsPointerOverUIObject()
        {
            // Check if the current touch or mouse position is over a UI object
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}

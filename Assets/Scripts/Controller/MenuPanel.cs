using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace controller
{
    public class MenuPanel : MonoBehaviour//the menu panel is responsible for handling the pause menu logic which is also the main menu
    {
        public GameObject pauseMenuUI;
        [SerializeField] Button PauseButton;
        [SerializeField] Button StartButton;
        [SerializeField] Button ResetButton;

        [SerializeField] TextMeshProUGUI StartText;
        bool isPaused;
        bool didResumeOnce;
        private void Start()
        {
            PauseButton.onClick.AddListener(TogglePauseMenu);
            StartButton.onClick.AddListener(TogglePauseMenu);
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
                    StartText.text = "Resume";
                    didResumeOnce = true;
                    ResetButton.gameObject.SetActive(true);
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

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace view
{

    public class InputHandler : IInputHandler, ITickable// Handles Input Detection
    {
        private int horInput;
        public bool isLeftPressed;
        public bool isRightPressed;
        public Action OnTapScreen { get; set; }
        [Inject(Id = ("Left"))] private IArrowButton leftArrowButton;
        [Inject(Id = ("Right"))] private IArrowButton rightArrowButton;

        private void CheckShoot()
        {
            if (CheckTouch() && !IsPointerOverUIObject())
            {
                OnTapScreen.Invoke();
                //SoundManager.Play(SoundManager.Sound.playerShoot);

                Debug.Log("Shoot");
            }
        }

        private void SetHorInput()
        {
            isLeftPressed = leftArrowButton.IsPointerDown;
            isRightPressed = rightArrowButton.IsPointerDown;
            if (isLeftPressed)
            {
                horInput = -1;
            }
            else if (isRightPressed)
            {
                horInput = 1;
            }
            else
            {
                horInput = 0;
            }

#if UNITY_EDITOR
            if (horInput == 0)
                horInput = (int)Input.GetAxisRaw("Horizontal");
#endif
        }
        public int GetHorInput() { return horInput; }
        private bool CheckTouch()
        {
            bool touchBegan = false;
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        touchBegan = true;
                    }
                }
            }

            if (Input.GetMouseButtonDown(0) || touchBegan)
            {
                return true;
            }
            return false;
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

        public void Tick()
        {
            SetHorInput();
            CheckShoot();
        }
    }

}

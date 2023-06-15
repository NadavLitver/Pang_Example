using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using view;

namespace controller
{

    public class InputHandler : MonoBehaviour// stores the information about player horizontal movement, if there was more complex movement such as jumping the input would also be here
    {
        private int horInput;
        public bool isLeftPressed;
        public bool isRightPressed;
        public UnityEvent onShoot;
        [SerializeField] LeftArrowButton leftArrowButton;
        [SerializeField] RightArrowButton rightArrowButton;
        private void Update()
        {
            SetHorInput();
            CheckShoot();
        }
        public void CheckShoot()
        {
            if (CheckTouch() && !IsPointerOverUIObject())
            {
                onShoot.Invoke();
                //SoundManager.Play(SoundManager.Sound.playerShoot);

                Debug.Log("Shoot");
            }
        }
        public void SetHorInput()
        {
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
        public bool CheckTouch()
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
        private bool IsPointerOverUIObject()//check that touch is not on ui object ( so you don't shoot when moving)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (var result in results)
            {
                if (result.gameObject == leftArrowButton.gameObject || result.gameObject == rightArrowButton.gameObject)
                {
                    return true; // Pointer is over the left or right arrow button
                }
            }
            return false; // Pointer is not over the left or right arrow button
        }
    }

}

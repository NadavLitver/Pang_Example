using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using controller;
using UnityEngine.EventSystems;

namespace view
{
    public class RightArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] InputHandler inputHandler;
        private bool isPointerDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
        }

        private void Update()
        {
            inputHandler.isRightPressed = isPointerDown;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using controller;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace view
{
    public class RightArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent<bool> RightPressedUpdateEvent;
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
            RightPressedUpdateEvent?.Invoke(isPointerDown);//update the input manager through an event to reduce dependency
            // inputHandler.isRightPressed = isPointerDown;
        }

    }
}

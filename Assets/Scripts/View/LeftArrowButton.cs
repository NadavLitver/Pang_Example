using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace view
{
    public class LeftArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool isPointerDown;
        public UnityEvent<bool> LeftPressedUpdateEvent;

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
            LeftPressedUpdateEvent?.Invoke(isPointerDown);//update the input manager through an event to reduce dependency
                                                          // inputHandler.isLeftPressed = isPointerDown;
        }

    }
}

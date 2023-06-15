using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace view
{
    public class RightArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
       
        public bool isPointerDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
        }

      

    }
}

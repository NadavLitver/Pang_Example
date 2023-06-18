using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace view
{
    public class ArrowButton : MonoBehaviour, IArrowButton//button that uses unity's Ipointer down and up the identify touches and clicks
    {
       
        public bool IsPointerDown { get; private set; }

      

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPointerDown = false;
        }

      

    }
}

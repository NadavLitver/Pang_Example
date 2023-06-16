using UnityEngine.EventSystems;

public interface IArrowButton : IPointerDownHandler, IPointerUpHandler
{
    bool IsPointerDown { get; }
}
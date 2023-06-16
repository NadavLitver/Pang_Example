using UnityEngine.Events;

public interface IInputHandler
{
    int GetHorInput();
    UnityEvent onShoot { get; }
}


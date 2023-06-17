using UnityEngine.Events;
namespace controller
{
    public interface IInputHandler
    {
        int GetHorInput();
        UnityEvent onShoot { get; }
        public void Init();
    }

}
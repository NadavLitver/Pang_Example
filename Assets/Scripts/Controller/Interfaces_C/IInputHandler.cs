using UnityEngine.Events;
namespace controller
{
    public interface IInputHandler
    {
        int GetHorInput();
        UnityEvent OnTapScreen { get; set; }
    }

}
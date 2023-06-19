using System;
namespace controller
{
    public interface IInputHandler
    {
        int GetHorInput();
        Action OnTapScreen { get; set; }
    }

}
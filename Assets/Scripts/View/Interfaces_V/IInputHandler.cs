using System;
namespace view
{
    public interface IInputHandler
    {
        int GetHorInput();
        Action OnTapScreen { get; set; }
    }

}
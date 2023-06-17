using UnityEngine;
using UnityEngine.Events;
namespace controller
{
    public interface IShootController
    {
        UnityEvent OnShot { get; }
    }
}
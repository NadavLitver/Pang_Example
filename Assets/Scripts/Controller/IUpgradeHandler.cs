using System.Collections;
using UnityEngine.Events;

namespace controller
{
    public interface IUpgradeHandler
    {
        IEnumerator UpgradeRoutine();
        UnityEvent<int> OnHPUpgraded { get; }
    }
}
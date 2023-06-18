using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine.Events;

namespace controller
{
    public interface IUpgradeHandler
    {
        UniTask UpgradeRoutine();
        UnityEvent<int> OnHPUpgraded { get; }
        public UnityEvent OnLasersUpgraded { get;  }

    }
}
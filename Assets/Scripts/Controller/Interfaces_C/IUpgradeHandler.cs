using Cysharp.Threading.Tasks;
using System.Collections;
using System;

namespace controller
{
    public interface IUpgradeHandler
    {
        UniTask UpgradeRoutine();
        Action<int> OnHPUpgraded { get; set; }
        Action OnLasersUpgraded { get; set; }

    }
}
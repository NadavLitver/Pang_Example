using System.Collections;

namespace controller
{
    public interface IUpgradeHandler
    {
        IEnumerator UpgradeRoutine();
    }
}
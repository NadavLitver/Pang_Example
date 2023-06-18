using model;
using UnityEngine;
using Zenject;
namespace controller
{
    public class RobotAnimatorInstaller : MonoInstaller
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer robotSR;
        [SerializeField] private Transform shootPoint;
        [SerializeField] RobotAnimatorUpdater robotAnimatorUpdaterPrefab;
        /// <summary>
        /// Install the components neccessary for the animator updater
        /// Create and install the updater from a prefab
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(shootPoint).WhenInjectedInto<RobotAnimatorUpdater>();
            Container.Bind<SpriteRenderer>().FromInstance(robotSR).WhenInjectedInto<RobotAnimatorUpdater>();
            Container.Bind<Animator>().FromInstance(animator).WhenInjectedInto<RobotAnimatorUpdater>();
            Container.Bind<IRobotAnimatorUpdater>().To<RobotAnimatorUpdater>().FromComponentInNewPrefab(robotAnimatorUpdaterPrefab).AsSingle();

        }
    }
}
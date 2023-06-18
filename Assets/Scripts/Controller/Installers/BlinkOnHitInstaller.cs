using UnityEngine;
using view;
using Zenject;
namespace controller
{

    public class BlinkOnHitInstaller : MonoInstaller
    {
        [SerializeField] BlinkOnHit blinkOnHit;
        [SerializeField] private SpriteRenderer robotSR;
        /// <summary>
        /// install the sprite renderer for Blink on Hit Class
        ///  create and install blink for hit from prefab
        /// </summary>
        public override void InstallBindings()
        {
          
            Container.Bind<SpriteRenderer>().FromInstance(robotSR).WhenInjectedInto<BlinkOnHit>();
          
            Container.Bind<IBlinkOnHit>().To<BlinkOnHit>().FromComponentInNewPrefab(blinkOnHit).AsSingle();

        }
    }
}
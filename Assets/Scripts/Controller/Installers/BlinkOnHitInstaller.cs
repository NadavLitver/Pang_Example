using UnityEngine;
using view;
using Zenject;
namespace controller
{

    public class BlinkOnHitInstaller : MonoInstaller
    {
        [SerializeField] BlinkOnHit blinkOnHit;
        [SerializeField] private SpriteRenderer robotSR;
        public override void InstallBindings()
        {
            //install the sprite renderer for Blink on Hit Class
            Container.Bind<SpriteRenderer>().FromInstance(robotSR).WhenInjectedInto<BlinkOnHit>();
            // install the blink on hit from the
            Container.Bind<IBlinkOnHit>().To<BlinkOnHit>().FromComponentInNewPrefab(blinkOnHit).AsSingle();

        }
    }
}
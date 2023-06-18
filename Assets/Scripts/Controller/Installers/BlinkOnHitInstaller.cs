using UnityEngine;
using view;
using Zenject;
namespace controller
{

    public class BlinkOnHitInstaller : MonoInstaller
    {
        
        [SerializeField] private SpriteRenderer robotSR;
        /// <summary>
        /// install the sprite renderer for Blink on Hit Class
        ///  create and install blink 
        /// </summary>
        public override void InstallBindings()
        {
          
            Container.Bind<SpriteRenderer>().FromInstance(robotSR).WhenInjectedInto<BlinkOnHit>();
          
            Container.Bind<IBlinkOnHit>().To<BlinkOnHit>().FromNew().AsSingle();

        }
    }
}
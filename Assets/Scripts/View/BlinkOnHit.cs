using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace view
{
    public class BlinkOnHit :IBlinkOnHit// created only to make a small blinking effect when player is damaged
    {
        [Inject] private SpriteRenderer robotSR;
        private Color blinkColor = new Color(1, 1, 1, 0.25f);
        private Color startingColor = Color.white;


        public void CallBlinkRoutine(int currentHealth)
        {
            if (currentHealth >= 0)
            {
               _= BlinkRoutine();
            }
        }

        private async UniTaskVoid BlinkRoutine()
        {
            robotSR.color = blinkColor;
            await UniTask.Delay(250);
            robotSR.color = startingColor;
            await UniTask.Delay(250);
            robotSR.color = blinkColor;
            await UniTask.Delay(250);
            robotSR.color = startingColor;
        }
    }
}

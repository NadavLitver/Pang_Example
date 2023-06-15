using controller;
using System.Collections;
using UnityEngine;
namespace view
{
    public class BlinkOnHit : MonoBehaviour// created only to make a small blinking effect when player is damaged
    {
        [SerializeField] SpriteRenderer robotSR;
        [SerializeField] Color blinkColor = new Color(1, 1, 1, 0.25f);
        private Color startingColor = Color.white;
      

        public void CallBlinkRoutine(int currentHealth)
        {
            if (currentHealth >= 0)
            {
                StartCoroutine(BlinkRoutine());
            }
        }
        IEnumerator BlinkRoutine()
        {
            robotSR.color = blinkColor;
            yield return new WaitForSeconds(0.25f);
            robotSR.color = startingColor;
            yield return new WaitForSeconds(0.25f);
            robotSR.color = blinkColor;
            yield return new WaitForSeconds(0.25f);
            robotSR.color = startingColor;
        }
    }
}

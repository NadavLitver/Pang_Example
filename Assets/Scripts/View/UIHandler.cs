using System.Collections;
using TMPro;
using UnityEngine;
namespace view
{
    public class UIHandler : MonoBehaviour// the UI handler is responsible for updating ui elements
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI healthText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] TextMeshProUGUI CountDownText;
        [SerializeField] TextMeshProUGUI EndingText;
      
        [SerializeField] GameObject EndingPanel;
        public void UpdateScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
        public void UpdateHealth(int health)
        {
            if (healthText != null)
            {
                healthText.text = "Health: " + health;
            }
        }
        public void UpdateLevel(int level)
        {
            if (levelText != null)
            {
                levelText.text = "Level: " + level;
            }
        }
        public void UpdateEnding(bool isWon)
        {
            if (EndingText != null)
            {
                EndingText.text = isWon ? "You Won !" : "You Lost !";
            }
        }
        public void EnableEndingPanel(bool isWon)
        {
            UpdateEnding(isWon);
            EndingPanel.SetActive(true);
        }
        public void CallCountdownRoutine(int level) => StartCoroutine(CountdownRoutine(level));
        IEnumerator CountdownRoutine(int level)
        {
            yield return new WaitForSeconds(0.05f);
            CountDownText.gameObject.SetActive(true);
            CountDownText.text = "3";
            yield return new WaitForSeconds(1);
            CountDownText.text = "2";
            yield return new WaitForSeconds(1);
            CountDownText.text = "1";
            yield return new WaitForSeconds(1);
            CountDownText.gameObject.SetActive(false);

        }
    }
}
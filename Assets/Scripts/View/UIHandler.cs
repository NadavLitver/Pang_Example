using System.Collections;
using TMPro;
using UnityEngine;
namespace view
{
    public class UIHandler : MonoBehaviour,IUIHandler// the UI handler is responsible for updating ui elements
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private TextMeshProUGUI endingText;
        [SerializeField] private TextMeshProUGUI startText;


        [SerializeField] private GameObject endingPanel;
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
            if (endingText != null)
            {
                endingText.text = isWon ? "You Won !" : "You Lost !";
            }
        }
        public void UpdateStartText()
        {
            if (startText != null)
            {
                startText.text = "Resume";
            }
        }
        public void EnableEndingPanel(bool isWon)
        {
            UpdateEnding(isWon);
            endingPanel.SetActive(true);
        }
        public void CallCountdownRoutine(int level) => StartCoroutine(CountdownRoutine(level));
        IEnumerator CountdownRoutine(int level)
        {
            yield return new WaitForSeconds(0.05f);
            countDownText.gameObject.SetActive(true);
            countDownText.text = "3";
            yield return new WaitForSeconds(1);
            countDownText.text = "2";
            yield return new WaitForSeconds(1);
            countDownText.text = "1";
            yield return new WaitForSeconds(1);
            countDownText.gameObject.SetActive(false);

        }
    }
}
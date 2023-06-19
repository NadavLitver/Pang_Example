using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace view
{
    public class UIHandler : IUIHandler// the UI handler is responsible for updating ui elements
    {
        [Inject(Id = "ScoreText")] private readonly TextMeshProUGUI scoreText;
        [Inject(Id = "HealthText")] private readonly TextMeshProUGUI healthText;
        [Inject(Id = "LevelText")] private readonly TextMeshProUGUI levelText;
        [Inject(Id = "CountDownText")] private readonly TextMeshProUGUI countDownText;
        [Inject(Id = "EndingText")] private readonly TextMeshProUGUI endingText;
        [Inject(Id = "StartText")] private readonly TextMeshProUGUI startText;
        [Inject(Id = "EndingPanel")] private readonly GameObject endingPanel;

        public Action<bool> OnEndingPanel { get; set; }

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
            OnEndingPanel.Invoke(true);
        }
        public void CallCountdownRoutine(int level) => _ = CountdownRoutine(level);

        private async UniTaskVoid CountdownRoutine(int level)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            countDownText.gameObject.SetActive(true);
            countDownText.text = "3";
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            countDownText.text = "2";
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            countDownText.text = "1";
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            countDownText.gameObject.SetActive(false);
        }
    }
}
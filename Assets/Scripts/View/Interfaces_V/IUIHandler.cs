using System;

namespace view
{
    public interface IUIHandler
    {
        void UpdateScore(int score);
        void UpdateHealth(int health);
        void UpdateLevel(int level);
        void UpdateEnding(bool isWon);
        void UpdateStartText();
        void EnableEndingPanel(bool isWon);
        void CallCountdownRoutine(int level);
        public Action<bool> OnEndingPanel { get; set; }
    }

}
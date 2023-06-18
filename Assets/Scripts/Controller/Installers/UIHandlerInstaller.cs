using TMPro;
using UnityEngine;
using view;
using Zenject;
namespace controller
{
  

    public class UIHandlerInstaller : MonoInstaller
    {
        [SerializeField] private UIHandler uiHandler;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private TextMeshProUGUI endingText;
        [SerializeField] private TextMeshProUGUI startText;
        [SerializeField] private GameObject endingPanel;
        /// <summary>
        /// install the ui handler from a new prefab
        /// install the corresponding texts using ID
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IUIHandler>().To<UIHandler>().FromComponentInNewPrefab(uiHandler).AsSingle().NonLazy();
            Container.Bind<TextMeshProUGUI>().WithId("ScoreText").FromInstance(scoreText);
            Container.Bind<TextMeshProUGUI>().WithId("HealthText").FromInstance(healthText);
            Container.Bind<TextMeshProUGUI>().WithId("LevelText").FromInstance(levelText);
            Container.Bind<TextMeshProUGUI>().WithId("CountDownText").FromInstance(countDownText);
            Container.Bind<TextMeshProUGUI>().WithId("EndingText").FromInstance(endingText);
            Container.Bind<TextMeshProUGUI>().WithId("StartText").FromInstance(startText);
            Container.Bind<GameObject>().WithId("EndingPanel").FromInstance(endingPanel);
        }
    }
}
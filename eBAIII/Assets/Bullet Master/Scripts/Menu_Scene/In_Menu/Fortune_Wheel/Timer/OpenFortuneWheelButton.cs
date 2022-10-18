using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Fortune_Wheel.Timer
{
    public class OpenFortuneWheelButton : MonoBehaviour
    {
        private AvailabilityTimer _availabilityTimer;

        [SerializeField]private GameObject fortunePanel;
        [SerializeField]private Button openFortune;

        private void Start()
        {
            _availabilityTimer = GetComponent<AvailabilityTimer>();
            openFortune.onClick.AddListener(OpenFortuneWheelPanel);
        }

        private void OpenFortuneWheelPanel()
        {
            //Open fortune wheel panel and restart timer
            if (!_availabilityTimer.isWheelReady) return;

            _availabilityTimer.RestartTimer();
            fortunePanel.SetActive(true);
        }
    }
}

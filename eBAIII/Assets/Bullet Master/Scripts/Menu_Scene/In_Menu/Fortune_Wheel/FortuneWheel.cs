using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Fortune_Wheel
{
    public class FortuneWheel : MonoBehaviour
    {
        private bool _claimed;
        
        [Header("Additionals scripts")]
        [SerializeField] private CoinsPanel coinsPanel;
        [SerializeField] private Wheel wheel;
        [SerializeField] private Arrow arrow;
        
        [Header("Buttons")]
        [SerializeField] private Button rotateWheel;
        [SerializeField] private Button claimReward;

        private void OnEnable()
        {
            //Enable default buttons
            rotateWheel.gameObject.SetActive(true);
            claimReward.gameObject.SetActive(false);

            rotateWheel.onClick.AddListener(OnRotateButtonClicked);

            _claimed = false;
        }

        private void OnRotateButtonClicked()
        {
            if (wheel.speed > 0) return;
            
            wheel.RotateWheel();
            wheel.RotateEnded += OnRotateWheelEnded;
        }

        private void OnRotateWheelEnded()
        {
            //When wheel stopped, enable claim reward button
            claimReward.gameObject.SetActive(true);
            rotateWheel.gameObject.SetActive(false);
            
            wheel.RotateEnded -= OnRotateWheelEnded;
            
            claimReward.onClick.AddListener(OnClaimRewardClick);
        }

        private void OnClaimRewardClick()
        {
            if (_claimed) return;

            //Save reward coins and disable panel
            coinsPanel.Coins = arrow.reward;
            claimReward.onClick.RemoveListener(OnClaimRewardClick);
            gameObject.SetActive(false);
            _claimed = true;
        }
    }
}

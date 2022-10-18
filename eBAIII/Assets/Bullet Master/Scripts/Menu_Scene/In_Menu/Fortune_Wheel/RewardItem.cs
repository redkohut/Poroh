using TMPro;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Fortune_Wheel
{
    public class RewardItem : MonoBehaviour
    {
        public int coins;
        [SerializeField] private TextMeshProUGUI rewardText;

        private void Start()
        {
            //Show reward coin count
            rewardText.text = "" + coins;
        }
    }
}

using System.Collections;
using System.Threading.Tasks;
using Bullet_Master.Scripts.Menu_Scene.Json;
using TMPro;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu
{
    public class CoinsPanel : MonoBehaviour
    {
        private const string CoinsSizing = "Sizing";
        private Animator _animator;
        
        [SerializeField] private TextMeshProUGUI text;
        
        private int _coins;
        public int Coins
        {
            get => _coins;
            set
            {
                if (_coins == value) return;

                SaveCoinsCount(value);
                
                StopAllCoroutines();
                StartCoroutine(ShowCoinsCount(value));
            }
        }

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            ShowCoinsCount();
        }

        private void ShowCoinsCount()
        {
            //Load coins from saves and show
            _coins = GameData.LoadData().Coins;
            text.text = _coins.ToString();
        }

        private void SaveCoinsCount(int value)
        {
            _coins += value;
            //Save new coins value
            var mainSaves = GameData.LoadData();
            mainSaves.Coins = _coins;
            GameData.SaveData(mainSaves);
        }

        private IEnumerator ShowCoinsCount(int value)
        {
            text.text = _coins.ToString();
            
            //Increasing the number of coins or decreasing it to look like an animation
            var coins = _coins - value;
            var substactingCoins = value > 0 ? 5 : -5;
            while (coins != _coins)
            {
                coins += substactingCoins;

                text.text = coins.ToString();
                _animator.Play(CoinsSizing);
                //Delay 1 millisecond, you can change it or change value of increasing coins, default = 5 
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}

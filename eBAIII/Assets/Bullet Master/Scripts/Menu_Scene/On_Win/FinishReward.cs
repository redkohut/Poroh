using Bullet_Master.Scripts.Menu_Scene.In_Game;
using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.On_Win
{
    public class FinishReward : MonoBehaviour
    {
        [SerializeField] private int defaultReward;
        [SerializeField] private TextMeshProUGUI coinsCount;

        [Header("Additional Scripts")] 
        [SerializeField] private CartridgesPanel cartridgesPanel;
        [SerializeField] private Sounds sounds;

        private void OnEnable()
        {
            var gameData = GameData.LoadData();
            
            //Calculate player reward depend used cartridges count
            var reward = defaultReward * cartridgesPanel.cartridgesCount;
            reward += defaultReward;

            coinsCount.text = reward.ToString();
            gameData.Coins += reward;
            GameData.SaveData(gameData);
            
            //Play finish sounds
            if(GameData.LoadData().Sounds && sounds.onWin != null)
                GetComponent<AudioSource>().PlayOneShot(sounds.onWin);
        }
    }
}

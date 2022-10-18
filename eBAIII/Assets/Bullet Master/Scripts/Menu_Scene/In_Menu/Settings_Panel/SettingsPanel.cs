using Bullet_Master.Scripts.Menu_Scene.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Settings_Panel
{
    public class SettingsPanel : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button sounds;
        [SerializeField] private Button vibrations;
        [SerializeField] private Button accept;

        [Header("Music")]
        [SerializeField] private Sprite soundsOn;
        [SerializeField] private Sprite soundsOff;
        
        [Header("Vibration")]
        [SerializeField] private Sprite vibrationsOn;
        [SerializeField] private Sprite vibrationsOff;

        private void Start()
        {
            sounds.onClick.AddListener(OnSoundsButtonClick);
            vibrations.onClick.AddListener(OnVibrationsButtonClick);
            accept.onClick.AddListener(OnAcceptButtonClick);
            
            SetButtonsIcons();
        }

        private void SetButtonsIcons()
        {
            var gameData = GameData.LoadData();
            sounds.image.sprite = gameData.Sounds ? soundsOn : soundsOff;
            vibrations.image.sprite = gameData.Vibrations ? vibrationsOn : vibrationsOff;
        }

        private void OnSoundsButtonClick()
        {
            var gameData = GameData.LoadData();
            gameData.Sounds = !gameData.Sounds;
            GameData.SaveData(gameData);
            
            sounds.image.sprite = gameData.Sounds ? soundsOn : soundsOff;
        }
        
        private void OnVibrationsButtonClick()
        {
            var gameData = GameData.LoadData();
            gameData.Vibrations = !gameData.Vibrations;
            GameData.SaveData(gameData);
            
            vibrations.image.sprite = gameData.Vibrations ? vibrationsOn : vibrationsOff;
        }
        
        private void OnAcceptButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}

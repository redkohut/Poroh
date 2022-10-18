using Bullet_Master.Scripts.Menu_Scene.Json;
using TMPro;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu
{
    public class LevelIdPanel : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            UpdateLevelIdText();
        }

        public void UpdateLevelIdText()
        {
            _text.text = "Level " + GameData.LoadData().LevelId;
        }
    }
}

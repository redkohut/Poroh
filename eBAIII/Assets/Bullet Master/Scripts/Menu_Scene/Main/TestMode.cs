using Bullet_Master.Scripts.Menu_Scene.Json;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.Main
{
    public class TestMode : MonoBehaviour
    { 
        [Header("If you need to run a specific level, turn it on and enter the level ID")]
        public bool isTestMode;
        [SerializeField] private int id = 1;

        private void Awake()
        {
            if (!isTestMode) return;
            
            var gameData = GameData.LoadData();
            gameData.LevelId = id;
            GameData.SaveData(gameData);
        }
    }
}

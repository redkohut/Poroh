using Bullet_Master.Scripts.Constants;
using Bullet_Master.Scripts.Level_Scene;
using Bullet_Master.Scripts.Menu_Scene.In_Game;
using Bullet_Master.Scripts.Menu_Scene.In_Menu;
using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.Menu_Scene.On_Win;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.Main
{
    public class GameManager : MonoBehaviour
    {
        private Enemies _enemies;
        private TestMode _testMode;

        [Header("Other Scripts")] public LevelIdPanel levelIdPanel;
        public CartridgesPanel cartridgesPanel;
        public FinishParticle finishParticle;

        [Header("Panels")] [SerializeField] private GameObject inMenu;
        public GameObject inGame;
        [SerializeField] private GameObject win;

        [Header("Buttons")] [SerializeField] private Button play;
        [SerializeField] private Button replay;
        [SerializeField] private Button nextLevel;
        [SerializeField] private Button allLevels;

        private void Start()
        {
            play.onClick.AddListener(OnPlayButtonClick);
            _testMode = GetComponent<TestMode>();
        }

        private void GetComponents()
        {
            _enemies = GameObject.Find(MainConstants.EnemiesPath).GetComponent<Enemies>();
        }

        private void SubscribeListeners()
        {
            GetComponents();
            _enemies.AllEnemiesDead += OnGameFinished;
            cartridgesPanel.AllCartridgesUsed += OnGameOver;
        }

        private void UnsubscribeListeners()
        {
            _enemies.AllEnemiesDead -= OnGameFinished;
            cartridgesPanel.AllCartridgesUsed -= OnGameOver;
        }

        private void OnPlayButtonClick()
        {
            //Enable game panel and subscribe listeners
            inMenu.SetActive(false);
            inGame.SetActive(true);

            play.onClick.RemoveListener(OnPlayButtonClick);
            replay.onClick.AddListener(OnReplayButtonClick);

            SubscribeListeners();
        }

        private void OnReplayButtonClick()
        {
            //Enable menu panel, and reload level
            inMenu.SetActive(true);
            inGame.SetActive(false);

            play.onClick.AddListener(OnPlayButtonClick);
            replay.onClick.RemoveListener(OnReplayButtonClick);

            replay.gameObject.SetActive(false);

            //Set values to default
            cartridgesPanel.SetCartridgesAsDefault();
            UnsubscribeListeners();
            
            LoadLevel();
        }

        private void OnNextLevelButtonClick()
        {
            //Enable menu panel, and load new level
            inMenu.SetActive(true);
            win.SetActive(false);

            play.onClick.AddListener(OnPlayButtonClick);
            nextLevel.onClick.RemoveListener(OnNextLevelButtonClick);

            //Set values to default
            cartridgesPanel.SetCartridgesAsDefault();
            UnsubscribeListeners();

            LoadLevel();

            levelIdPanel.UpdateLevelIdText();
        }

        private void OnGameOver()
        {
            //If player lose all cartridges, show replay button
            if (_enemies.IsAllEnemiesDead()) return;
            replay.gameObject.SetActive(true);
        }

        private void OnGameFinished()
        {
            //Enable win panel and show finish particle, upgrade level id
            win.SetActive(true);
            inGame.SetActive(false);

            finishParticle.ShowParticle();
            UpgradeLevelId();

            replay.onClick.RemoveListener(OnReplayButtonClick);
            nextLevel.onClick.AddListener(OnNextLevelButtonClick);
        }

        private void UpgradeLevelId()
        {
            //Upgrade level id if it isn't test mode
            var gameData = GameData.LoadData();
            if (!_testMode.isTestMode)
            {
                var levelsCount = SceneManager.sceneCountInBuildSettings - 1;
                gameData.LevelId = levelsCount > gameData.LevelId++ ? gameData.LevelId++ : 1;
            }
            GameData.SaveData(gameData);
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(GameData.LoadData().LevelId);
        }
    }
}

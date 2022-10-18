using Bullet_Master.Scripts.Menu_Scene.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bullet_Master.Scripts.Menu_Scene.Main
{
    public class LoadLevelScene : MonoBehaviour
    {
        #region Test Mode
        [Space(6)]
        [Header("\tSet the required sceneId")]
        [Space(6)]
        [SerializeField] private bool isTestingScene = false;
        [SerializeField] private int sceneIdTest = 0;
        #endregion
        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (isTestingScene) // testing mode / when create a scene
            {
                GameData gameData = new GameData();
                gameData.LevelId = sceneIdTest;
                GameData.SaveData(gameData);
                SceneManager.LoadScene(GameData.LoadData().LevelId);
            }
            else
            {
                SceneManager.LoadScene(GameData.LoadData().LevelId);
            }
        }
    }
}

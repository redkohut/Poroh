using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.Json
{
    public class GameData
    {
        public int LevelId = 1;
        public int Coins = 0;
        public ulong TimerTime = 0;

        public bool Sounds = true;
        public bool Vibrations = true;

        public int SkinId = 0;

        public List<int> Skins = new List<int>();

        public static void SaveData(GameData gameData)
        {
            var path = Application.persistentDataPath + "_MainSaves";
            var json = JsonUtility.ToJson(gameData);
            
            //Save game data as json to game persistent path
            File.WriteAllText(path, json);
        }

        public static GameData LoadData()
        {
            if (File.Exists(Application.persistentDataPath + "_MainSaves"))
            {
                var path = Application.persistentDataPath + "_MainSaves";
                var json = File.ReadAllText(path);
                
                //Load game data from json
                return JsonUtility.FromJson<GameData>(json);
            }
            else
                return new GameData();
        }

        public void SetNecessaryScene(int sceneId)
        {
            LevelId = sceneId;
        }
    }
}

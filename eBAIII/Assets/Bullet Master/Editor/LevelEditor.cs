using System.Collections.Generic;
using Bullet_Master.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bullet_Master.Editor
{
    public class LevelEditor : EditorWindow
    {
        private const string ScenesPath = "Assets/Bullet Master/Scenes/";

        public GameObject playerCamera, environment, enimies, player, enemy;
        public Cartridges cartridges;

        private int _sceneCount, _windowId;
        private Scene _scene;
        private List<GameObject> _spawnedGameObjects = new List<GameObject>();

        private Texture2D _image1;
        
        private void OnEnable()
        {
            _sceneCount = SceneManager.sceneCountInBuildSettings - 1;
        }

        private void OnGUI()
        {
            switch (_windowId)
            {
                case 0:

                    GUILayout.Label("This editor can create a new level for you with ready-made objects.", EditorStyles.boldLabel);
                    GUILayout.Label("There are " + _sceneCount + " levels in the game now.", EditorStyles.boldLabel);
                    
                    //Load preview texture
                    _image1 = Resources.Load("image1", typeof(Texture2D)) as Texture2D;
                    GUILayout.Label(_image1, GUILayout.Width(250), GUILayout.Height(250));

                    if (GUI.Button(new Rect(10, 310 + EditorGUIUtility.singleLineHeight, 180, 60), "Create new level"))
                        CreateNewLevel();

                    if (GUI.Button(new Rect(200, 310 + EditorGUIUtility.singleLineHeight, 100, 60), "Close"))
                        Close();
                    break;
                case 1:
                    GUILayout.Label("You need to add your new scene to Scenes In Build.", EditorStyles.boldLabel);

                    if (GUILayout.Button("Open Settings", GUILayout.Width(100), GUILayout.Height(50))) 
                    {
                        //Open the Add Scenes In Build window
                        GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
                        _windowId++;
                    }
                    break;
                case 2:
                    GUILayout.Label("This completes the setup.", EditorStyles.boldLabel);
                    GUILayout.Label("Now you need to manually redesign your level.", EditorStyles.boldLabel);
                    
                    if (GUI.Button(new Rect(160, 180 + EditorGUIUtility.singleLineHeight, 80, 40), "Close"))
                    {
                        Close();
                        _windowId++;
                    }
                    break;
            }

            if (!GUI.changed) return;
            //Save changes
            foreach (var spawnedGameObject in _spawnedGameObjects)
                EditorUtility.SetDirty(spawnedGameObject);
            EditorSceneManager.MarkSceneDirty(_scene);
        }

        private void CreateNewLevel()
        {
            CreateNewScene();
            InstantiateDefaultObjects();
            AddDefaultCartridgesValue();
            _windowId++;
        }
        
        private void CreateNewScene()
        {
            //Create, save, and open new scene
            var levelId = _sceneCount + 1;

            _scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            EditorSceneManager.SaveScene(_scene, ScenesPath + "Level_" + levelId + ".unity", false);
            EditorSceneManager.OpenScene(ScenesPath + "Level_" + levelId + ".unity");
        }

        private void AddDefaultCartridgesValue()
        {
            //Add default cartridges value for new level
            cartridges.cartridgesPerLevelCount.Add(3);
        }
        
        private void InstantiateDefaultObjects()
        {
            //Instantiating default level objects
            
            _spawnedGameObjects.Add((GameObject)PrefabUtility.InstantiatePrefab(playerCamera));
            _spawnedGameObjects.Add((GameObject)PrefabUtility.InstantiatePrefab(environment));
            _spawnedGameObjects.Add((GameObject)PrefabUtility.InstantiatePrefab(enimies));
            _spawnedGameObjects.Add((GameObject)PrefabUtility.InstantiatePrefab(player));
            _spawnedGameObjects.Add((GameObject)PrefabUtility.InstantiatePrefab(enemy));
        }
        


        [MenuItem("Level Editor/Open Editor")]
        public static void ObjectsPanel() //Open the level editor by clicking on the button in the menu
        {
           GetWindowWithRect(typeof(LevelEditor), new Rect(0, 0, 400, 400));
        }

        [MenuItem("Level Editor/Documentation")]
        private static void Documentation()//Opening the documentation in a browser
        {
            Application.OpenURL("https://drive.google.com/file/d/1hNdEJ6sZWmWRL5Qm8VdRnsPGFlNECWEn/view");
        }
    }
}

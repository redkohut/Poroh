using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.LevelsPanel
{
    public class LevelButton : MonoBehaviour
    {
        [Header("\tMenu parent and LevelPanel")]
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject levelsPanel;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
            levelsPanel.SetActive(!levelsPanel.activeSelf);
        }
    }
}

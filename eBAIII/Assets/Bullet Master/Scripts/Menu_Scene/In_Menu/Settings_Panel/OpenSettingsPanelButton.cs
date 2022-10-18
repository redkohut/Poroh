using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Settings_Panel
{
    public class OpenSettingsPanelButton : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnOpenButtonClick);
        }

        private void OnOpenButtonClick()
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}

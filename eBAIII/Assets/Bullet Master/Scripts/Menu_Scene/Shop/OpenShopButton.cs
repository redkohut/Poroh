using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.Shop
{
    public class OpenShopButton : MonoBehaviour
    {
        [SerializeField] private GameObject shop;
        
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OpenShop);
        }

        private void OpenShop()
        {
            shop.SetActive(true);
        }
    }
}

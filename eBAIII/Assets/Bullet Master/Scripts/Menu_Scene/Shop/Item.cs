using System;
using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.Shop
{
    public enum ItemState
    {
        Selling,
        Bought,
        Equipped,
    }

    public class Item : MonoBehaviour
    {
        public Action ItemEquipped;
        public Action<int> ItemBought;
        
        private ItemState _itemState;

        [Header("Item Info")]
        [SerializeField] private int id;
        public int cost;

        [Header("Button Objects")]
        [SerializeField] private Button action;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image coin;
        
        [Header("Skin Images")]
        [SerializeField] private Image head;
        [SerializeField] private Image body;
        [SerializeField] private Image[] arms;
        [SerializeField] private Image[] hands;
        [SerializeField] private Image[] legs;
        [SerializeField] private Image[] feet;
        
        [Header("Additional Scripts")] 
        [SerializeField]private PlayerSkins playerSkins;

        private void OnEnable()
        {
            SetSkin();
        }
        
        private void SetSkin()
        {
            //Get skin depend id
            PlayerSkins.Skin skin = playerSkins.skins[id];

            //Set skin sprites to item
            head.sprite = skin.head;
            body.sprite = skin.body;
            foreach (var arm in arms)
                arm.sprite = skin.arm;
            foreach (var hand in hands)
                hand.sprite = skin.hand;
            foreach (var leg in legs)
                leg.sprite = skin.leg; 
            foreach (var foot in feet)
                foot.sprite = skin.foot;
        }

        public void CheckItemStatus(GameData gameData)
        {
            //Check item state
            _itemState = gameData.Skins[id] == 0 ? ItemState.Selling : ItemState.Bought;
            _itemState = gameData.SkinId == id ? ItemState.Equipped : _itemState;

            switch (_itemState)
            {
                case ItemState.Selling:
                    Selling();
                    break;
                case ItemState.Bought:
                    Bought();
                    break;
                case ItemState.Equipped:
                    Equipped();
                    break;
            }
        }

        private void Selling()
        {
            title.text = cost.ToString();
            action.onClick.RemoveAllListeners();
            action.onClick.AddListener(OnBuyButtonClick);
        }

        private void Bought()
        {
            title.text = "Equip";
            coin.gameObject.SetActive(false);
            
            action.onClick.RemoveAllListeners();
            action.onClick.AddListener(OnEquipButtonClick);
        }

        private void Equipped()
        {
            title.text = "Equipped";
            coin.gameObject.SetActive(false);
            
            action.onClick.RemoveAllListeners();
        }

        private void OnBuyButtonClick()
        {
            if (_itemState == ItemState.Bought || _itemState == ItemState.Equipped) return;
            
            var gameData = GameData.LoadData();
            if (gameData.Coins < cost) return;
            //Save new data for this item
            gameData.Skins[id] = 1;
            gameData.SkinId = id;
            GameData.SaveData(gameData);

            ItemBought?.Invoke(-cost);
            ItemEquipped?.Invoke();
        }

        private void OnEquipButtonClick()
        {
            //Save new data for this item
            var gameData = GameData.LoadData();
            gameData.SkinId = id;
            GameData.SaveData(gameData);

            //Check other items buttons state
            CheckItemStatus(gameData);
            ItemEquipped?.Invoke();
        }
    }
}

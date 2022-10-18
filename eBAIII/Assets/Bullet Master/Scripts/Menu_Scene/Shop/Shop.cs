using System;
using System.Net.WebSockets;
using Bullet_Master.Scripts.Constants;
using Bullet_Master.Scripts.Level_Scene.Player;
using Bullet_Master.Scripts.Menu_Scene.In_Menu;
using Bullet_Master.Scripts.Menu_Scene.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.Shop
{
    public class Shop : MonoBehaviour
    {
        private Skin _skin;

        [SerializeField] private Button close;
        [Header("Shop Items Scripts")]
        [SerializeField] private Item[] items;
        [Header("Additional Scripts")]
        [SerializeField] private CoinsPanel coinsPanel;

        private void OnEnable()
        {
            GetComponents();
            Startup();
            SubscribeActions();
        }

        private void OnDisable()
        {
            UnsubscribeActions();
        }

        private void GetComponents()
        {
            _skin = GameObject.FindGameObjectWithTag(MainConstants.PlayerTag).GetComponent<Skin>();
        }
        
        private void Startup()
        {
            var gameData = GameData.LoadData();
            if (gameData.Skins.Count == 0)
                //If haven't saves, create default saves
                SetDefaultItemStatuses(gameData);
            else
                CheckItemsStatuses();
        }
        
        private void SubscribeActions()
        {
            //Subscribe all items actions
            foreach (var item in items)
            {
                item.ItemEquipped += CheckItemsStatuses;
                item.ItemBought += SubstractSkinPrice;
            }
            
            close.onClick.AddListener(CloseShop);
        }

        private void UnsubscribeActions()
        {
            //Unsubscribe all items actions
            foreach (var item in items)
            {
                item.ItemEquipped -= CheckItemsStatuses;
                item.ItemBought -= SubstractSkinPrice;
            }
            
            close.onClick.RemoveListener(CloseShop);
        }

        private void SubstractSkinPrice(int value)
        {
            //Decrease item price value from coins
            coinsPanel.Coins = value;
        }
        
        private void CheckItemsStatuses()
        {
            //Check all item statuses when shop opening
            var gameData = GameData.LoadData();
            foreach (var item in items)
                item.CheckItemStatus(gameData);
            _skin.SetSkin();
        }
        
        private void SetDefaultItemStatuses(GameData gameData)
        {
            //Set default item status for all items and save it
            foreach (var item in items)
            {
                gameData.Skins.Add(item.cost == 0 ? 1 : 0);
                item.CheckItemStatus(gameData);
            }
            GameData.SaveData(gameData);
        }

        private void CloseShop()
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections.Generic;
using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet_Master.Scripts.Menu_Scene.In_Game
{
    public class CartridgesPanel : MonoBehaviour
    {
        public Action AllCartridgesUsed;
        
        [HideInInspector]public int cartridgesCount;
        private readonly List<Image> _cartridges = new List<Image>();
        private readonly List<GameObject> _spawnedBullets = new List<GameObject>();

        [Header("Other Objects")] 
        [SerializeField] private Sprite blackCartridge;
        [SerializeField] private Sprite goldCartridge;
        [SerializeField] private GameObject prefab;

        [Header("Additional Scripts")] 
        [SerializeField] private Cartridges cartridges;
        
        private void OnEnable()
        {
            //Instantiate cartridges depend cartridges count for this level
            InstantiateCartridges();
        }

        private void DestroyInstantiatedCartridges()
        {
            //Destroy all images and remove al from list
            foreach (var cartridge in _cartridges)
                Destroy(cartridge.gameObject);
            _cartridges.Clear();
        }

        private void InstantiateCartridges()
        {
            //Instantiate cartridges images and add to list
            DestroyInstantiatedCartridges();
            for (var num = 0; num < cartridges.cartridgesPerLevelCount[GameData.LoadData().LevelId]; num++)
            {
                var cartridge = Instantiate(prefab, transform);
                _cartridges.Add(cartridge.GetComponent<Image>());
            }
            cartridgesCount = _cartridges.Count;
        }

        public void AddBullet(GameObject bullet)
        {
            //Add new bullet to list of spawned bullets
            _spawnedBullets.Add(bullet);
            //Reduce the number of cartridges
            cartridgesCount--;
            //And update sprites of images
            ChangeCartridgesImages();
        }

        public void RemoveBullet(GameObject bullet)
        {
            //When bullet destroyed, remove from list and check this's last bullet or not
            _spawnedBullets.Remove(bullet);
            if(cartridgesCount == 0 && _spawnedBullets.Count == 0)
                AllCartridgesUsed?.Invoke();
        }

        private void ChangeCartridgesImages()
        {
            //Set sprites to images depend used cartridges count
            for (var num = 1; num <= _cartridges.Count; num++)
                _cartridges[num-1].sprite = num <= cartridgesCount ? goldCartridge : blackCartridge;
        }

        public void SetCartridgesAsDefault()
        {
            //Set default cartridges sprites
            cartridgesCount = _cartridges.Count;
            ChangeCartridgesImages();
        }
    }
}

using System;
using UnityEngine;

namespace Bullet_Master.Scripts.Level_Scene
{
    public class Enemies : MonoBehaviour
    {
        public Action AllEnemiesDead;
        private int _deadEnemiesCount;

        [Header("Paste here all level enemies")]
        public Enemy.Enemy[] enemies;


        public int DeadEnemiesCount
        {
            get => _deadEnemiesCount;
            set
            {
                _deadEnemiesCount = value;
                
                //Invoke action when all enemies dead
                if(value == enemies.Length)
                    AllEnemiesDead?.Invoke();
            }
        }

        public bool IsAllEnemiesDead()
        {
            //Return true or false, depend alive enemies
            return _deadEnemiesCount == enemies.Length;
        }
    }
}

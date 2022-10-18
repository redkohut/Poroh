using System;
using Bullet_Master.Scripts.Constants;
using Bullet_Master.Scripts.Menu_Scene.In_Game;
using Bullet_Master.Scripts.Menu_Scene.Main;
using UnityEngine;

namespace Bullet_Master.Scripts.Level_Scene.Player
{
    public class Bullet : MonoBehaviour
    {
        private float _time;
        private CartridgesPanel _cartridgesPanel;
        [HideInInspector] public Vector2 direction;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float destroyTime = 4;
        [SerializeField] private GameObject bloodPrefab;


        private void Start()
        {
            _cartridgesPanel = GameObject.Find(MainConstants.CanvasPath).GetComponent<GameManager>().cartridgesPanel;
            _cartridgesPanel.AddBullet(gameObject);

        }

        private void Update()
        {
            //Destroy bullet when time will pass
            _time += Time.deltaTime;
            if (_time > destroyTime)
                Destroy(gameObject);


            //Move bullet depend direction
            transform.Translate(direction * bulletSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //If bullet collided with either object, but not enemy - reflect direction
            if(other.gameObject.CompareTag(MainConstants.BoxTag))
                Destroy(gameObject);
            else if (other.gameObject.CompareTag(MainConstants.EnemyTag))
                //If collided with enemy spawn blood particle
                Instantiate(bloodPrefab, other.contacts[0].point, bloodPrefab.transform.rotation, other.transform);
            else
                direction = Vector3.Reflect(direction, other.contacts[0].normal);
        }

        private void OnDestroy()
        {
            //On destroy bullet, remove it from spawned bullets list in cartridges panel
            _cartridgesPanel.RemoveBullet(gameObject);
        }
    }
}

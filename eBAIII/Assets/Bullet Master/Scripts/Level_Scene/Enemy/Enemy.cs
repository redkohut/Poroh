using Bullet_Master.Scripts.Constants;
using Bullet_Master.Scripts.Level_Scene.Player;
using UnityEngine;

namespace Bullet_Master.Scripts.Level_Scene.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private bool _isDead;
        private Rigidbody2D _rigidbody2D;
        private Enemies _enemies;
        
        [Header("Repulsion force when hit by a bullet or a box")]
        [SerializeField] private float bouncingPower;

        private void Start()
        {
            GetComponents();
        }

        private void GetComponents()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemies = GameObject.Find(MainConstants.EnemiesPath).GetComponent<Enemies>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //If collided object bullet, enemy dead and destroy the bullet, but else only dead without destroy collided object
            if (other.CompareTag(MainConstants.BulletTag))
            {
                Dead(other.gameObject.GetComponent<Bullet>().direction);
                Destroy(other.gameObject);
            }
            else if(other.CompareTag(MainConstants.BoxTag))
                Dead(other.gameObject.GetComponent<Box>().rb.velocity.normalized);
        }

        private void Dead(Vector3 direction)
        {
            //The enemy can only die once
            if (_isDead) return;
            
            _rigidbody2D.constraints = RigidbodyConstraints2D.None;
            
            //Throw it aside, depending on the direction of the bullet or box
            _rigidbody2D.AddForce(direction * bouncingPower, ForceMode2D.Impulse);

            _isDead = true;
            _enemies.DeadEnemiesCount++;
        }
    }
}
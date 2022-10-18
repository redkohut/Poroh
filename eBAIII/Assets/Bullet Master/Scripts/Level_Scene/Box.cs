using Bullet_Master.Scripts.Constants;
using Bullet_Master.Scripts.Level_Scene.Player;
using UnityEngine;

namespace Bullet_Master.Scripts.Level_Scene
{
    public class Box : MonoBehaviour
    {
        [HideInInspector]public Rigidbody2D rb;

        [SerializeField] private float power;
        [SerializeField] private GameObject bloodPrefab;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //When box collided with bullet, add force depend bullet direction
            if (other.gameObject.CompareTag(MainConstants.BulletTag))
                rb.AddForce(other.gameObject.GetComponent<Bullet>().direction * power, ForceMode2D.Impulse);
            //When collided with enemy, spawn blood particle
            else if(other.gameObject.CompareTag(MainConstants.EnemyTag))
                Instantiate(bloodPrefab, other.contacts[0].point, bloodPrefab.transform.rotation, other.transform);
            //When box collided with another box
            else if(other.gameObject.CompareTag(MainConstants.BoxTag))
                rb.AddForce(rb.velocity.normalized * power, ForceMode2D.Impulse);
        }
    }
}

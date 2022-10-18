using Bullet_Master.Plugins;
using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.Menu_Scene.Main;
using Bullet_Master.Scripts.ScriptableObjects;
using UnityEngine;

namespace Bullet_Master.Scripts.Level_Scene.Player
{
    public class Gun : MonoBehaviour
    {
        private const string CanvasPath = "Content/Canvas";
        
        private bool _mouseButtonDown;
        private AudioSource _audioSource;
        private GameManager _gameManager;

        [SerializeField]private LineRenderer lineRenderer;
        [SerializeField] private GameObject bulletPrefab;
        
        [Header("Gun Pivots")]
        [SerializeField] private Transform gunStart;
        [SerializeField] private Transform gunEnd;

        [Header("Additional Scripts")]
        [SerializeField] private Sounds sounds;

        private void Start()
        {
            Input.multiTouchEnabled = false;
            GetComponents();
        }

        private void GetComponents()
        {
            _gameManager = GameObject.Find(CanvasPath).GetComponent<GameManager>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!_gameManager.inGame.activeSelf) return;

            if (Input.GetMouseButtonDown(0))
                _mouseButtonDown = true;
            
            if(Input.GetMouseButton(0))
            {
                DrawLaser();
                Rotation();
            }
            if (Input.GetMouseButtonUp(0) && _mouseButtonDown)
            {
                _mouseButtonDown = false;
                if (_gameManager.cartridgesPanel.cartridgesCount == 0) return;
                Strike();
                RemoveLaser();
            }
        }

        private void Rotation()
        {
            //Calculate direction between gun position and touch position
            Vector2 direction = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Calculate angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Set angle to player arm pivot
            Quaternion quaternion = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = quaternion;
        }

        private void DrawLaser()
        {
            //Create raycast hit
            RaycastHit2D hit = Physics2D.Raycast(gunEnd.position, transform.TransformDirection(Vector3.down), 10f);
            if (hit)
            {
                //Draw laser line
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                //Draw ray to find the end point for ray
                Ray ray = new Ray(gunEnd.position, transform.TransformDirection(Vector3.down) * 10f);
                //Draw line
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, ray.GetPoint(10));   
            }
        }
    
        private void RemoveLaser()
        {
            lineRenderer.positionCount = 0;
        }
    
        private void Strike()
        {
            GameObject bullet = Instantiate(bulletPrefab, gunEnd.position, Quaternion.identity);
            //Calculate bullet direction
            Vector3 bulletDirection = (gunEnd.position - gunStart.position).normalized;
            //Set direction to bullet
            bullet.GetComponent<Bullet>().direction = bulletDirection;
            
            if(GameData.LoadData().Sounds && sounds.shot != null)
                _audioSource.PlayOneShot(sounds.shot);
            Vibration.Vibrate(50);
        }
    }
}

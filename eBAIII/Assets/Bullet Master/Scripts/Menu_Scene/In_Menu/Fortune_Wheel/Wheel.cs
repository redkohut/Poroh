using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Fortune_Wheel
{
    public class Wheel : MonoBehaviour
    {
        public Action RotateEnded;
        [HideInInspector]public float speed;
        
        [Header("Settings")]
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float stoppingSpeed;

        private void OnEnable()
        {
            transform.eulerAngles = Vector3.zero;
        }

        public void RotateWheel()
        {
            //Calculate random speed to get different rewards
            speed = Random.Range(rotationSpeed - 200, rotationSpeed + 200);
        }

        private void Update()
        {
            if (speed > 0)
            {
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                speed -= stoppingSpeed;
            }
            else
                RotateEnded?.Invoke();
        }
    }
}

using Bullet_Master.Plugins;
using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.ScriptableObjects;
using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.In_Menu.Fortune_Wheel
{
    public class Arrow : MonoBehaviour
    {
        private const string ArrowAnimationName = "Tick";
        private AudioSource _audioSource;
        
        [HideInInspector] public int reward = 300;
        [SerializeField] private Animator animator;
        
        [Header("Additional Scripts")]
        [SerializeField] private Wheel wheel;
        [SerializeField] private Sounds sounds;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var coinsReward = other.GetComponent<RewardItem>().coins;
            
            //When wheel rotating and reward changed, play animation, sound and vibrate
            if (coinsReward != reward && wheel.speed > 0)
            {
                reward = coinsReward;
                animator.Play(ArrowAnimationName);
                
                Vibration.Vibrate(20);
                if(GameData.LoadData().Sounds && sounds.fortuneWheelClick != null)
                     _audioSource.PlayOneShot(sounds.fortuneWheelClick);
            }
        }
    }
}

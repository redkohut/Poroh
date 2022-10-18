using UnityEngine;

namespace Bullet_Master.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Sounds", menuName = "Bullet Master/Sounds")]
    public class Sounds : ScriptableObject
    {
        public AudioClip onWin;
        public AudioClip shot;
        public AudioClip fortuneWheelClick;
    }
}
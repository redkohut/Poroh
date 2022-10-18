using UnityEngine;

namespace Bullet_Master.Scripts.Menu_Scene.On_Win
{
    public class FinishParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;

        public void ShowParticle()
        {
            particle.Play();
        }
    }
}

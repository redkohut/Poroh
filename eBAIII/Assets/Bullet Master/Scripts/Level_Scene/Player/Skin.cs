using Bullet_Master.Scripts.Menu_Scene.Json;
using Bullet_Master.Scripts.ScriptableObjects;
using UnityEngine;

namespace Bullet_Master.Scripts.Level_Scene.Player
{
    public class Skin : MonoBehaviour
    {
        [SerializeField] private PlayerSkins playerSkins;
        [Header("Player sprite renderers")]
        [SerializeField] private SpriteRenderer head;
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private SpriteRenderer[] arms;
        [SerializeField] private SpriteRenderer[] hands;
        [SerializeField] private SpriteRenderer[] legs;
        [SerializeField] private SpriteRenderer[] feet;

        private void Start()
        {
            SetSkin();
        }

        public void SetSkin()
        {
            //Get skin depend id
            int skinId = GameData.LoadData().SkinId;
            PlayerSkins.Skin skin = playerSkins.skins[skinId];

            //Set skin sprites to player
            head.sprite = skin.head;
            body.sprite = skin.body;
            foreach (var arm in arms)
                arm.sprite = skin.arm;
            foreach (var hand in hands)
                hand.sprite = skin.hand;
            foreach (var leg in legs)
                leg.sprite = skin.leg; 
            foreach (var foot in feet)
                foot.sprite = skin.foot;
        }
    }
}

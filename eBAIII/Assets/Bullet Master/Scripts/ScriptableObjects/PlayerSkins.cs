using System;
using UnityEngine;

namespace Bullet_Master.Scripts.ScriptableObjects
{
   [CreateAssetMenu(fileName = "PlayerSkins", menuName = "Bullet Master/PlayerSkins")]
   public class PlayerSkins : ScriptableObject
   {
      public Skin[] skins;
      
      [Serializable]
      public class Skin
      {
         public Sprite head;
         public Sprite body;
         public Sprite arm;
         public Sprite hand;
         public Sprite leg;
         public Sprite foot;
      }
   }
}

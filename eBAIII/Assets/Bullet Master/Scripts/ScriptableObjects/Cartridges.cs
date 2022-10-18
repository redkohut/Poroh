using System.Collections.Generic;
using UnityEngine;

namespace Bullet_Master.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Cartridges", menuName = "Bullet Master/Cartridges")]
    public class Cartridges : ScriptableObject
    {
        public List<int> cartridgesPerLevelCount;
    }
}
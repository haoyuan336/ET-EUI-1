using UnityEngine;

namespace ET.Client
{
    public class FindEnemyComponent : Entity, IAwake<int>, IUpdate
    {
        public int ColliderLayer;

        public AIComponent AIComponent;

        public int FindAngle;

    }
    
}